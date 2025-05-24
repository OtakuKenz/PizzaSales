using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class PizzaService(PizzaSalesContext context) : IPizzaService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<ImportResultDto> ImportAsync(ImportRequest request)
    {
      using var stream = request.File.OpenReadStream();
      using var reader = new StreamReader(stream);

      var config = new CsvConfiguration(CultureInfo.InvariantCulture);
      using var csv = new CsvReader(reader, config);

      csv.Context.RegisterClassMap<PizzaMap>();
      var pizzas = csv.GetRecords<Pizza>().ToList();

      var existingIds = _context.Pizzas
        .Select(p => p.PizzaId)
        .ToHashSet();

      var newPizzas = pizzas
        .Where(p => !existingIds.Contains(p.PizzaId))
        .ToList();

      // Ensure EF does not try to insert/update PizzaType
      foreach (var pizza in newPizzas)
      {
        pizza.PizzaType = null;
      }

      int inserted = newPizzas.Count;
      int duplicates = pizzas.Count - inserted;

      _context.Pizzas.AddRange(newPizzas);
      await _context.SaveChangesAsync();

      return new ImportResultDto
      {
        Inserted = inserted,
        Duplicates = duplicates
      };
    }
  }

}