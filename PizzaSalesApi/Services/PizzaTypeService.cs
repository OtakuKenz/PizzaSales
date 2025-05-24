using CsvHelper;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;
using PizzaSalesApi.Models.DTOs;
using System.Globalization;

namespace PizzaSalesApi.Services
{
  public class PizzaTypeService(PizzaSalesContext context) : IPizzaTypeService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<ImportResultDto> ImportAsync(PizzaTypeImportRequest request)
    {
      using var stream = request.File.OpenReadStream();
      using var reader = new StreamReader(stream);

      var config = new CsvConfiguration(CultureInfo.InvariantCulture);
      using var csv = new CsvReader(reader, config);

      csv.Context.RegisterClassMap<PizzaTypeMap>();
      var pizzaTypes = csv.GetRecords<PizzaType>().ToList();

      var existingIds = _context.PizzaTypes
        .Select(pt => pt.PizzaTypeId)
        .ToHashSet();

      var newPizzaTypes = pizzaTypes
        .Where(pt => !existingIds.Contains(pt.PizzaTypeId))
        .ToList();

      int inserted = newPizzaTypes.Count;
      int duplicates = pizzaTypes.Count - inserted;

      _context.PizzaTypes.AddRange(newPizzaTypes);
      await _context.SaveChangesAsync();

      return new ImportResultDto
      {
        Inserted = inserted,
        Duplicates = duplicates
      };
    }
  }
}