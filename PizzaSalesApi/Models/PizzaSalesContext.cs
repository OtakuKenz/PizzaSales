using Microsoft.EntityFrameworkCore;

namespace PizzaSalesApi.Models
{
  public class PizzaSalesContext(DbContextOptions<PizzaSalesContext> options) : DbContext(options)
  {
    public DbSet<Order> Orders { get; set; }
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<PizzaType> PizzaTypes { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
  }
}