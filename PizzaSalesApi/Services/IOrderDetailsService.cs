using System;
using PizzaSalesApi.Models.DTOs;

namespace PizzaSalesApi.Services
{
  public interface IOrderDetailsService
  {
    /// <summary>
    /// Imports order details from a CSV file using the provided import request.
    /// </summary>
    /// <param name="request">The import request containing the file.</param>
    /// <returns>Import result with inserted and duplicate counts.</returns>
    Task<ImportResultDto> ImportAsync(ImportRequest request);

    /// <summary>
    /// Retrieves the details of a specific order by its ID.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<OrderDetailDto> GetOrderDetailsAsync(int orderId);
  }
}
