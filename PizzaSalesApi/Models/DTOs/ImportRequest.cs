namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a request to import pizza types from a file.
  /// </summary>
  public class ImportRequest
  {
    /// <summary>
    /// File containing pizza type data for import.
    /// </summary>
    public IFormFile File { get; set; } = default!;
  }
}