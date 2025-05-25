using CsvHelper;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;
using PizzaSalesApi.Models.DTOs;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace PizzaSalesApi.Services
{
  public class PizzaTypeService(PizzaSalesContext context) : IPizzaTypeService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<ImportResultDto> ImportAsync(ImportRequest request)
    {
      using var stream = request.File.OpenReadStream();
      using var reader = new StreamReader(stream);

      var config = new CsvConfiguration(CultureInfo.InvariantCulture);
      using var csv = new CsvReader(reader, config);

      csv.Context.RegisterClassMap<PizzaTypeMap>();
      var pizzaTypes = csv.GetRecords<PizzaTypeCsvDto>().ToList();

      var existingIds = _context.PizzaTypes
        .Select(pt => pt.PizzaTypeId)
        .ToHashSet();

      var newPizzaTypesDtos = pizzaTypes
        .Where(pt => !existingIds.Contains(pt.PizzaTypeId))
        .ToList();

      // Map the DTOs to the PizzaType entities
      var newPizzaTypes = newPizzaTypesDtos.Select(dto => new PizzaType
      {
        PizzaTypeId = dto.PizzaTypeId,
        Name = dto.Name
      }).ToList();

      // Count the number of inserted and duplicate records
      int inserted = newPizzaTypes.Count;
      int duplicates = pizzaTypes.Count - inserted;

      // Add the new PizzaType entities to the database.
      _context.PizzaTypes.AddRange(newPizzaTypes);
      await _context.SaveChangesAsync();

      foreach (var dto in newPizzaTypesDtos)
      {
        // Save the pizza category
        var pizzaTypeCategories = GetPizzaTypeCategory(dto);
        _context.PizzaTypeCategories.Add(pizzaTypeCategories);
        await _context.SaveChangesAsync();

        // Save the pizza ingredients
        var pizzaTypeIngredients = GetPizzaIngredients(dto);
        _context.PizzaTypeIngredients.AddRange(pizzaTypeIngredients);
        await _context.SaveChangesAsync();
      }

      return new ImportResultDto
      {
        Inserted = inserted,
        Duplicates = duplicates
      };
    }

    /// <summary>
    /// Helper method to get the ingredients for a pizza type based on the provided DTO and database records.
    /// </summary>
    /// <param name="dto">Pizza Type CSV DTO</param>
    /// <returns>List of PizzaTypeIngredient entities</returns>
    private List<PizzaTypeIngredient> GetPizzaIngredients(PizzaTypeCsvDto dto)
    {
      // Split the ingredients string into a list of ingredient names
      var ingredients = dto.Ingredients
        .Split(',')
        .Select(i => i.Trim())
        .ToList();

      var pizzaTypeIngredients = new List<PizzaTypeIngredient>();

      // For each ingredient name, check if it exists in the database
      // If it does not exist, create a new Ingredient entity
      foreach (var ingredientName in ingredients)
      {
        var ingredient = _context.Ingredients
          .FirstOrDefault(i => i.Name == ingredientName);

        if (ingredient == null)
        {
          ingredient = new Ingredient { Name = ingredientName };
          _context.Ingredients.Add(ingredient);
          _context.SaveChanges();
        }

        pizzaTypeIngredients.Add(new PizzaTypeIngredient
        {
          PizzaTypeId = dto.PizzaTypeId,
          IngredientId = ingredient.IngredientId
        });
      }

      return pizzaTypeIngredients;
    }

    /// <summary>
    /// Helper method to get or create a PizzaTypeCategory based on the provided DTO and record in the database.
    /// </summary>
    /// <param name="dto">Pizza Type CSV DTO</param>
    /// <returns>PizzaTypeCategory Entity</returns>
    private PizzaTypeCategory GetPizzaTypeCategory(PizzaTypeCsvDto dto)
    {
      var category = _context.Categories
        .FirstOrDefault(c => c.Name == dto.Category);

      if (category == null)
      {
        category = new Category { Name = dto.Category };
        _context.Categories.Add(category);
        _context.SaveChanges();
      }

      return new PizzaTypeCategory
      {
        PizzaTypeId = dto.PizzaTypeId,
        CategoryId = category.CategoryId
      };
    }
  }
}