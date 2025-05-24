using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaSalesApi.Models.Entities
{
  public class Category
  {
    /// <summary>
    /// Unique identifier for the category.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    /// <summary>
    /// Name of the category.
    /// </summary>
    [Required]
    public required string Name { get; set; }
  }
}
