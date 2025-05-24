using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PizzaSalesApi.Models
{

  /// <summary>
  /// Represents an order entity.
  /// </summary>
  public class Order
  {
    /// <summary>
    /// Unique identifier for the order.
    /// Database generated identity.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    /// <summary>
    /// Order date.
    /// </summary>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Order time.
    /// </summary>
    [Required]
    public TimeOnly Time { get; set; }
  }
}