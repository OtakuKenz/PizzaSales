using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaSalesApi.Models
{
  /// <summary>
  /// Represents the type of pizza.
  /// </summary>
  public class PizzaType
  {
    /// <summary>
    /// Unique identifier for the pizza type.
    /// </summary>
    /// 
    [Key]
    [JsonPropertyName("pizza_type_id")]
    public string PizzaTypeId { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string Ingredients { get; set; } = string.Empty;
  }
}