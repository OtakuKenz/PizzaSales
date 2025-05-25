namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a summary of an order with basic details for API responses.
  /// </summary>
  public class OrderSummaryDto
  {
    public int OrderId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }

    public decimal OrderTotal { get; set; }
  }
}