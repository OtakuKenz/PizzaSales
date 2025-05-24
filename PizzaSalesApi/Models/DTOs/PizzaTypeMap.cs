using System;
using CsvHelper.Configuration;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Models.DTOs;

/// <summary>
/// Used to map CSV columns to PizzaType properties.
/// </summary>
public class PizzaTypeMap : ClassMap<PizzaType>
{
  public PizzaTypeMap()
  {
    Map(m => m.PizzaTypeId).Name("pizza_type_id");
    Map(m => m.Name).Name("name");
    Map(m => m.Category).Name("category");
    Map(m => m.Ingredients).Name("ingredients");
  }
}
