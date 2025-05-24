using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class OrderService(PizzaSalesContext context) : IOrderService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<ImportResultDto> ImportAsync(ImportRequest request)
    {
      using var stream = request.File.OpenReadStream();
      using var reader = new StreamReader(stream);

      var config = new CsvConfiguration(CultureInfo.InvariantCulture);
      using var csv = new CsvReader(reader, config);

      csv.Context.RegisterClassMap<OrderMap>();
      var orders = csv.GetRecords<Order>().ToList();

      var existingIds = _context.Orders
        .Select(o => o.OrderId)
        .ToHashSet();

      var newOrders = orders
        .Where(o => !existingIds.Contains(o.OrderId))
        .ToList();

      int inserted = newOrders.Count;
      int duplicates = orders.Count - inserted;

      _context.Orders.AddRange(newOrders);
      await _context.SaveChangesAsync();

      return new ImportResultDto
      {
        Inserted = inserted,
        Duplicates = duplicates
      };
    }
  }

}