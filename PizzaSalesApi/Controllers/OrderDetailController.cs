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
  }
}
