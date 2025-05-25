namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents a simplified representation of Pizza per order.
  /// </summary>
  public class PizzaDto
  {
    /// <summary>
    /// Name of the pizza
    /// </summary>
    public string Pizza { get; set; } = string.Empty;

    /// <summary>
    /// List of ingredients for the pizza
    /// </summary>
    public List<string> Ingredients { get; set; } = [];

    /// <summary>
    /// Price of the pizza
    /// </summary>
    public decimal Price { get; set; }


    /// <summary>
    /// Size of the pizza
    /// </summary>
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the pizza in the order
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Subtotal for the pizza in the order
    /// </summary>
    public decimal Subtotal { get; set; }
  }

}