namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Data transfer object for exporting pizza types to CSV.
  /// </summary>
  public class PizzaTypeCsvDto
  {
    /// <summary>
    /// Unique identifier for the pizza type.
    /// </summary>
    public string PizzaTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Name of the pizza type.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Category of the pizza type, e.g., "Vegetarian", "Meat", etc.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated list of ingredient names for the pizza type.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty; // comma-separated
  }
}