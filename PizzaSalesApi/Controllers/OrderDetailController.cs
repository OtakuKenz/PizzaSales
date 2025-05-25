using Microsoft.AspNetCore.Mvc;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Services;

namespace PizzaSalesApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderDetailController(IOrderDetailsService orderDetailsService) : ControllerBase
  {
    private readonly IOrderDetailsService _orderDetailsService = orderDetailsService;

    [HttpPost("import")]
    public async Task<IActionResult> ImportOrderDetails([FromForm] ImportRequest request)
    {
      if (request.File == null || request.File.Length == 0)
        return BadRequest("No file uploaded.");

      var result = await _orderDetailsService.ImportAsync(request);
      return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderDetails(int orderId)
    {
      if (orderId <= 0)
        return BadRequest("Invalid order ID.");

      try
      {
        var orderDetails = await _orderDetailsService.GetOrderDetailsAsync(orderId);
        return Ok(orderDetails);
      }
      catch (ArgumentException ex)
      {
        return NotFound(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
}
