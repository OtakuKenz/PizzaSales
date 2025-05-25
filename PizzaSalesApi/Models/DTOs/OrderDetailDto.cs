using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PizzaSalesApi.Models.Entities;

namespace PizzaSalesApi.Models.DTOs
{
  /// <summary>
  /// Represents the details of an order in the Pizza Sales system for API Responses.
  /// </summary>
  public class OrderDetailDto
  {
    public OrderSummaryDto OrderSummary { get; set; } = new();

    public List<PizzaDto> Pizzas { get; set; } = new();
  }

}