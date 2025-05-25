namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a pizza sales report with ranking and pizza details.
  /// </summary>
  public class PizzaSalesReportDto
  {
    /// <summary>
    /// Represents the total sales amount for the pizza in the sales report.
    /// </summary>
    public decimal TotalSales { get; set; }

    /// <summary>
    /// Represents the name of the pizza in the sales report.
    /// </summary>
    public string Pizza { get; set; } = string.Empty;

    /// <summary>
    /// Represents the start date of the sales report period.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Represents the end date of the sales report period.
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Represents the quantity of pizzas sold in the report.
    /// </summary>
    public int Quantity { get; set; }
  }
}
