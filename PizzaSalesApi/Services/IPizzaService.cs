using PizzaSalesApi.Models.DTOs;

namespace PizzaSalesApi.Services
{
  /// <summary>
  /// Service interface for pizza type operations.
  /// </summary>
  public interface IPizzaService
  {
    /// <summary>
    /// Imports pizza from a CSV file using the provided import request.
    /// </summary>
    /// <param name="request">The import request containing the file.</param>
    /// <returns>Import result with inserted and duplicate counts.</returns>
    Task<ImportResultDto> ImportAsync(ImportRequest request);

    /// <summary>
    /// Retrieves a report of pizza sales with ranking and details.
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<List<PizzaSalesReportDto>> GetPizzaSalesReportAsync(DateTime? startDate, DateTime? endDate);

    /// <summary>
    /// Calculates the total sales amount for all pizzas within the specified date range.
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<decimal> GetTotalSalesAsync(DateTime? startDate, DateTime? endDate);
  }
}
