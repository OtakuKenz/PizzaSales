using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Models.Entities;
using PizzaSalesApi.Services;

namespace PizzaSalesApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController(IOrderService orderService) : ControllerBase
  {
    private readonly IOrderService _orderService = orderService;

    [HttpPost("import")]
    public async Task<IActionResult> ImportOrders([FromForm] ImportRequest request)
    {
      if (request.File == null || request.File.Length == 0)
        return BadRequest("No file uploaded.");

      var result = await _orderService.ImportAsync(request);
      return Ok(result);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders([FromQuery] SearchParamDto searchParams)
    {
        if (searchParams.PageNumber < 1) searchParams.PageNumber = 1;
        if (searchParams.PageSize < 1) searchParams.PageSize = 20;

        var pagedResult = await _orderService.GetAllOrdersAsync(searchParams);
        return Ok(pagedResult);
    }
  }
}
