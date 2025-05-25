using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Services
{
  public class OrderService(PizzaSalesContext context) : IOrderService
  {
    private readonly PizzaSalesContext _context = context;

    public async Task<PagedResultDto<OrderSummaryDto>> GetAllOrdersAsync(
        SearchParamDto searchParams)
    {
      var orderSummariesQuery = _context.Orders
          .Include(od => od.OrderDetails)
            .ThenInclude(od => od.Pizza).IgnoreAutoIncludes()
          .Select(result => new OrderSummaryDto
          {
            OrderTotal = result.OrderDetails.Sum(z => z.Quantity * z.Pizza.Price),
            OrderId = result.OrderId,
            Date = result.Date,
            Time = result.Time
          });

      if (!string.IsNullOrEmpty(searchParams.OrderNumber))
      {
        orderSummariesQuery = orderSummariesQuery.Where(od => od.OrderId.ToString().Contains(searchParams.OrderNumber));
      }

      if (searchParams.From.HasValue)
      {
        DateOnly dateFrom = DateOnly.FromDateTime(searchParams.From.Value);
        orderSummariesQuery = orderSummariesQuery.Where(od => od.Date >= dateFrom);
      }
      if (searchParams.To.HasValue)
      {
        DateOnly dateTo = DateOnly.FromDateTime(searchParams.To.Value);
        orderSummariesQuery = orderSummariesQuery.Where(od => od.Date <= dateTo);
      }

      var totalCount = await orderSummariesQuery.CountAsync();

      int pageNumber = searchParams.PageNumber.HasValue && searchParams.PageNumber.Value > 0 ? searchParams.PageNumber.Value : 1;
      int pageSize = searchParams.PageSize > 0 ? searchParams.PageSize : 20;

      var items = new List<OrderSummaryDto>();

      // Perform SQL sorting for date
      // Otherwise perform in-memory sorting for decimal due to SQL limitations
      if (searchParams.SortBy == "date")
      {
        orderSummariesQuery = searchParams.SortDirection == "desc"
            ? orderSummariesQuery.OrderByDescending(od => od.Date)
            : orderSummariesQuery.OrderBy(od => od.Date);

        items = await orderSummariesQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
      }
      else
      {
        items = await orderSummariesQuery.ToListAsync();
        if (searchParams.SortBy == "orderTotal")
        {
          items = searchParams.SortDirection == "desc"
              ? items.OrderByDescending(od => od.OrderTotal).ToList()
              : items.OrderBy(od => od.OrderTotal).ToList();
        }
        else if (searchParams.SortBy == "orderId")
        {
          items = searchParams.SortDirection == "desc"
            ? items.OrderByDescending(od => od.OrderId).ToList()
            : items.OrderBy(od => od.OrderId).ToList();
        }
        items = items
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToList();
      }

      return new PagedResultDto<OrderSummaryDto>
      {
        TotalRecords = totalCount,
        Data = items,
        PageNumber = pageNumber,
        PageSize = pageSize
      };
    }

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