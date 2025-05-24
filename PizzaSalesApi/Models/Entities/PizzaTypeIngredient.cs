using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaSalesApi.Models.Entities
{
  /// <summary>
  /// Represents the relationship between a pizza type and its ingredients.
  /// </summary>
  public class PizzaTypeIngredient
  {
    /// <summary>
    /// Unique identifier for the pizza type ingredient.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PizzaTypeIngredientId { get; set; }

    /// <summary>
    /// Pizza type primary key.
    /// </summary>
    [ForeignKey(nameof(PizzaType))]
    [Required]
    public required string PizzaTypeId { get; set; }

    /// <summary>
    /// Reference to the associated pizza type.
    /// </summary>
    public virtual PizzaType? PizzaType { get; set; }

    /// <summary>
    /// Ingredient primary key.
    /// </summary>
    [ForeignKey(nameof(Ingredient))]
    [Required]
    public int IngredientId { get; set; }

    /// <summary>
    /// Reference to the associated ingredient.
    /// </summary>
    public virtual Ingredient? Ingredient { get; set; }
  }
}


