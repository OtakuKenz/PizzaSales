using Microsoft.EntityFrameworkCore;

namespace PizzaSalesApi.Models.Entities
{
  /// <summary>
  /// Represents the context for the Pizza Sales database.
  /// </summary>
  /// <param name="options"></param>
  public class PizzaSalesContext(DbContextOptions<PizzaSalesContext> options) : DbContext(options)
  {
    /// <summary>
    /// Represents the collection of Orders in the database.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Represents the collection of Customers in the database.
    /// </summary>
    public DbSet<Pizza> Pizzas { get; set; }

    /// <summary>
    /// Represents the collection of PizzaTypes in the database.
    /// </summary>
    public DbSet<PizzaType> PizzaTypes { get; set; }

    /// <summary>
    /// Represents the collection of OrderDetails in the database.
    /// </summary>
    public DbSet<OrderDetail> OrderDetails { get; set; }

    /// <summary>
    /// Represents the collection of Ingredients in the database.
    /// </summary>
    public DbSet<Ingredient> Ingredients { get; set; }

    /// <summary>
    /// Represents the collection of PizzaTypeIngredients in the database.
    /// </summary>
    public DbSet<PizzaTypeIngredient> PizzaTypeIngredients { get; set; }

    /// <summary>
    /// Represents the collection of Categories in the database.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Represents the collection of PizzaTypeCategories in the database.
    /// </summary>
    public DbSet<PizzaTypeCategory> PizzaTypeCategories { get; set; }
  }
}