using Microsoft.AspNetCore.Mvc;
using PizzaSalesApi.Models.DTOs;
using PizzaSalesApi.Services;

namespace PizzaSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PizzaController(IPizzaService pizzaService) : ControllerBase
	{
		private readonly IPizzaService _pizzaService = pizzaService;

		[HttpPost("import")]
		public async Task<IActionResult> ImportPizzaTypes([FromForm] ImportRequest request)
		{
			if (request.File == null || request.File.Length == 0)
				return BadRequest("No file uploaded.");

			var result = await _pizzaService.ImportAsync(request);
			return Ok(result);
		}
	}
}