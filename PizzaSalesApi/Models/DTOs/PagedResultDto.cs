namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a paginated result set.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class PagedResultDto<T>
  {
    /// <summary>
    /// Total number of records in the result set.
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Current page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// List of items in the current page.
    /// </summary>
    public List<T> Data { get; set; } = [];
  }
}