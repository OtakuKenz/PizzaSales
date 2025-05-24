using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaSalesApi.Models.Entities
{
  /// <summary>
  /// Represents the relationship between a pizza type and its category.
  /// </summary>
  public class PizzaTypeCategory
  {
    /// <summary>
    /// Unique identifier for the pizza type category.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PizzaTypeCategoryId { get; set; }

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
    /// Category primary key.
    /// </summary>
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    /// <summary>
    /// Reference to the associated category.
    /// </summary>
    public virtual Category? Category { get; set; }
  }
}