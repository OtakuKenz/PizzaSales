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
  }
}
