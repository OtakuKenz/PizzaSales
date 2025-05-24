using PizzaSalesApi.Models.DTOs;

namespace PizzaSalesApi.Services
{
  public interface IOrderService
  {
    /// <summary>
    /// Imports order from a CSV file using the provided import request.
    /// </summary>
    /// <param name="request">The import request containing the file.</param>
    /// <returns>Import result with inserted and duplicate counts.</returns>
    Task<ImportResultDto> ImportAsync(ImportRequest request);
  }
}


