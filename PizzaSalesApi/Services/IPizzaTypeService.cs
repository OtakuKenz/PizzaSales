using PizzaSalesApi.Models.DTOs;

namespace PizzaSalesApi.Services
{
  /// <summary>
  /// Service interface for pizza type operations.
  /// </summary>
  public interface IPizzaTypeService
  {
    /// <summary>
    /// Imports pizza types from a CSV file using the provided import request.
    /// </summary>
    /// <param name="request">The import request containing the file and header mapping.</param>
    /// <returns>Import result with inserted and duplicate counts.</returns>
    Task<ImportResultDto> ImportAsync(PizzaTypeImportRequest request);
  }
}
