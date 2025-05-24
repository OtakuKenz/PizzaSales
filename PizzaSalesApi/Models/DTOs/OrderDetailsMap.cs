using System;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Used to map CSV columns to OrderDetails properties.
  /// </summary>
  public class OrderDetailsMap : ClassMap<OrderDetail>
  {
    public OrderDetailsMap()
    {
      Map(m => m.OrderDetailId).Name("order_details_id");
      Map(m => m.OrderId).Name("order_id");
      Map(m => m.PizzaId).Name("pizza_id");
      Map(m => m.Quantity).Name("quantity");
    }
  }
}