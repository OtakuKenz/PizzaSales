using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class PizzaService(PizzaSalesContext context) : IPizzaService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<List<PizzaSalesReportDto>> GetPizzaSalesReportAsync(DateTime? startDate, DateTime? endDate)
    {
      var orderDetailsQuery = _context.OrderDetails
        .Include(od => od.Order)
        .Include(od => od.Pizza)
          .ThenInclude(p => p.PizzaType)
        .Where(od => od.Pizza != null && od.Pizza.PizzaType != null);

      if (startDate.HasValue)
      {
        var startDateOnly = DateOnly.FromDateTime(startDate.Value);
        orderDetailsQuery = orderDetailsQuery.Where(od => od.Order.Date >= startDateOnly);
      }
      if (endDate.HasValue)
      {
        var endDateOnly = DateOnly.FromDateTime(endDate.Value);
        orderDetailsQuery = orderDetailsQuery.Where(od => od.Order.Date <= endDateOnly);
      }

      var pizzaSalesReport = await orderDetailsQuery
      .GroupBy(z => z.PizzaId)
      .Select(od => new PizzaSalesReportDto
      {
        Pizza = od.First().Pizza.PizzaType!.Name,
        Quantity = od.Sum(x => x.Quantity),
        TotalSales = od.Sum(x => x.Quantity * x.Pizza.Price),
        StartDate = od.Min(x => x.Order.OrderDetails.Min(od => od.Order.Date)),
        EndDate = od.Max(x => x.Order.OrderDetails.Max(od => od.Order.Date)),
      })
      .ToListAsync();

      pizzaSalesReport = pizzaSalesReport.OrderByDescending(z => z.TotalSales)
                          .Take(50)
                          .ToList();

      return pizzaSalesReport;
    }

    public Task<decimal> GetTotalSalesAsync(DateTime? startDate, DateTime? endDate)
    {
      var orderDetailsQuery = _context.OrderDetails
        .Include(od => od.Order)
        .Include(od => od.Pizza)
          .ThenInclude(p => p.PizzaType)
        .Where(od => od.Pizza != null && od.Pizza.PizzaType != null);

      if (startDate.HasValue)
      {
        var startDateOnly = DateOnly.FromDateTime(startDate.Value);
        orderDetailsQuery = orderDetailsQuery.Where(od => od.Order.Date >= startDateOnly);
      }
      if (endDate.HasValue)
      {
        var endDateOnly = DateOnly.FromDateTime(endDate.Value);
        orderDetailsQuery = orderDetailsQuery.Where(od => od.Order.Date <= endDateOnly);
      }

      var pizzaSalesReport = orderDetailsQuery
            .SumAsync(z => z.Quantity * z.Pizza.Price);

      return pizzaSalesReport;
    }

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