using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaSalesApi.Models.Entities
{
  /// <summary>
  /// Represents the pizza entity.
  /// </summary>
  public class Pizza
  {
    /// <summary>
    /// Unique identifier for the pizza.
    /// </summary>
    [Key]
    [JsonPropertyName("pizza_id")]
    public string PizzaId { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key to the pizza type.
    /// </summary>
    [ForeignKey(nameof(PizzaType))]
    [JsonPropertyName("pizza_type_id")]
    public string PizzaTypeId { get; set; } = string.Empty;

    [Required]
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// Price of the pizza.
    /// Minimum value is 0.
    /// </summary>
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    /// <summary>
    /// Reference to <see cref="Model.PizzaType"/> 
    /// </summary>
    public virtual PizzaType PizzaType { get; set; } = new();
  }
}