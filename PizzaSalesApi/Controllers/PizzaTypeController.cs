using Microsoft.AspNetCore.Mvc;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Services;

namespace PizzaSalesApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PizzaTypeController(IPizzaTypeService pizzaTypeService) : ControllerBase
  {
    private readonly IPizzaTypeService _pizzaTypeService = pizzaTypeService;

    [HttpPost("import")]
    public async Task<IActionResult> ImportPizzaTypes([FromForm] PizzaTypeImportRequest request)
    {
      if (request.File == null || request.File.Length == 0)
        return BadRequest("No file uploaded.");

      var result = await _pizzaTypeService.ImportAsync(request);
      return Ok(result);
    }
  }
}