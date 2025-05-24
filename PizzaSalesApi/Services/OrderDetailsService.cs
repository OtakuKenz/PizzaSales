using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class OrderDetailsService(PizzaSalesContext context) : IOrderDetailsService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<ImportResultDto> ImportAsync(ImportRequest request)
    {
      using var stream = request.File.OpenReadStream();
      using var reader = new StreamReader(stream);

      var config = new CsvConfiguration(CultureInfo.InvariantCulture);
      using var csv = new CsvReader(reader, config);

      csv.Context.RegisterClassMap<OrderDetailsMap>();
      var orderDetails = csv.GetRecords<OrderDetail>().ToList();

      var existingIds = _context.OrderDetails
        .Select(o => o.OrderDetailId)
        .ToHashSet();

      var newOrderDetails = orderDetails
        .Where(o => !existingIds.Contains(o.OrderDetailId))
        .ToList();

      // Ensure EF does not try to insert/update foreign key references
      foreach (var orderDetail in newOrderDetails)
      {
        orderDetail.Order = null;
        orderDetail.Pizza = null;
      }

      int inserted = newOrderDetails.Count;
      int duplicates = orderDetails.Count - inserted;

      _context.OrderDetails.AddRange(newOrderDetails);
      await _context.SaveChangesAsync();

      return new ImportResultDto
      {
        Inserted = inserted,
        Duplicates = duplicates
      };
    }
  }

}