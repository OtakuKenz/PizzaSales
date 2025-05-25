namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a search query for a paginated result set.
  /// </summary>
  public class SearchParamDto
  {
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? OrderNumber { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; } = string.Empty;
    public string? SortDirection { get; set; } // "asc" or "desc"
    public int? PageNumber { get; set; }
  }
}
