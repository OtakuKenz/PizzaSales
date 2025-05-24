using System;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Used to map CSV columns to Pizza properties.
  /// </summary>
  public class PizzaMap : ClassMap<Pizza>
  {
    public PizzaMap()
    {
      Map(m => m.PizzaId).Name("pizza_id");
      Map(m => m.PizzaTypeId).Name("pizza_type_id");
      Map(m => m.Size).Name("size");
      Map(m => m.Price).Name("price");
    }
  }
}