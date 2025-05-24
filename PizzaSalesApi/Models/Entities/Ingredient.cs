using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaSalesApi.Models.Entities
{
  /// <summary>
  /// Represents an ingredient entity.
  /// </summary>
  public class Ingredient
  {
    /// <summary>
    /// Unique identifier for the ingredient.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IngredientId { get; set; }

    /// <summary>
    /// Name of the ingredient.
    /// </summary>
    [Required]
    public required string Name { get; set; }
  }
}
