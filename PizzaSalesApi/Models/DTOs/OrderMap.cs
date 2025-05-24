using System;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Used to map CSV columns to Order properties.
  /// </summary>
  public class OrderMap : ClassMap<Order>
  {
    public OrderMap()
    {
      Map(m => m.OrderId).Name("order_id");
      Map(m => m.Date).Name("date");
      Map(m => m.Time).Name("time");
    }
  }
}