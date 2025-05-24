using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaSalesApi.Models
{
  /// <summary>
  /// Represents order details.
  /// </summary>
  public class OrderDetail
  {
    /// <summary>
    /// Unique identifier for the order detail.
    /// Database generated identity.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("order_details_id")]
    public int OrderDetailId { get; set; }

    /// <summary>
    /// Order primary key.
    /// </summary>
    [ForeignKey(nameof(Order))]
    [JsonPropertyName("order_id")]
    [Required]
    public int OrderId { get; set; }

    /// <summary>
    /// Pizza primary key.
    /// </summary>
    [ForeignKey(nameof(Pizza))]
    [JsonPropertyName("pizza_id")]
    [Required]
    public required string PizzaId { get; set; }

    /// <summary>
    /// Item quantity in the order.
    /// Minimum value is 1.
    /// </summary>
    [Range(1, int.MaxValue)]
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Reference to <see cref="Models.Order"/>
    /// </summary>
    public virtual Order Order { get; set; } = new();

    /// <summary>
    /// Reference to <see cref="Models.Pizza"/>
    /// </summary>
    public virtual Pizza Pizza { get; set; } = new();
  }
}