using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaSalesApi.Models.Entities
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

    /// <summary>
    /// Name of the pizza type.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Collection of ingredients associated with the pizza type.
    /// </summary>
    public virtual ICollection<PizzaTypeIngredient> PizzaTypeIngredients { get; set; } = [];

    /// <summary>
    /// Collection of categories associated with the pizza type.
    /// </summary>
    public virtual ICollection<PizzaTypeCategory> PizzaTypeCategories { get; set; } = [];
  }
}