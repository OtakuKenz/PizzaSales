using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class OrderDetailsService(PizzaSalesContext context) : IOrderDetailsService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<OrderDetailDto> GetOrderDetailsAsync(int orderId)
    {
      var order = await _context.Orders
          .Where(o => o.OrderId == orderId)
          .Include(o => o.OrderDetails)
              .ThenInclude(od => od.Pizza)
                  .ThenInclude(p => p.PizzaType)
                      .ThenInclude(pt => pt.PizzaTypeIngredients)
                          .ThenInclude(pti => pti.Ingredient)
          .FirstOrDefaultAsync();

      if (order == null)
        throw new ArgumentException($"Order with ID {orderId} does not exist.");

      var pizzas = order.OrderDetails.Select(od =>
      {
        var pizzaType = od.Pizza?.PizzaType;
        var ingredients = pizzaType?.PizzaTypeIngredients?
              .Select(pti => pti.Ingredient?.Name ?? string.Empty)
              .ToList() ?? new List<string>();

        return new PizzaDto
        {
          Pizza = pizzaType?.Name ?? string.Empty,
          Price = od.Pizza?.Price ?? 0,
          Quantity = od.Quantity,
          Size = od.Pizza?.Size ?? string.Empty,
          Subtotal = od.Quantity * (od.Pizza?.Price ?? 0),
          Ingredients = ingredients
        };
      }).ToList();

      return new OrderDetailDto
      {
        OrderSummary = new OrderSummaryDto
        {
          OrderId = order.OrderId,
          Date = order.Date,
          Time = order.Time,
          OrderTotal = order.OrderDetails.Sum(od => od.Quantity * (od.Pizza?.Price ?? 0))
        },
        Pizzas = pizzas
      };
    }

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
        // Warning supressed to allow null assignment for foreign keys
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        orderDetail.Order = null;
        orderDetail.Pizza = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
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