using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models;

namespace PizzaSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PizzaController : ControllerBase
	{
		private readonly PizzaSalesContext _context;

		public PizzaController(PizzaSalesContext context)
		{
			_context = context;
		}

		// GET: api/Pizza
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
		{
			return await _context.Pizzas.ToListAsync();
		}

		// GET: api/Pizza/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Pizza>> GetPizza(string id)
		{
			var pizza = await _context.Pizzas.FindAsync(id);

			if (pizza == null)
			{
				return NotFound();
			}

			return pizza;
		}

		// PUT: api/Pizza/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPizza(string id, Pizza pizza)
		{
			if (id != pizza.PizzaId)
			{
				return BadRequest();
			}

			_context.Entry(pizza).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PizzaExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Pizza
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
		{
			_context.Pizzas.Add(pizza);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (PizzaExists(pizza.PizzaId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetPizza", new { id = pizza.PizzaId }, pizza);
		}

		// DELETE: api/Pizza/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePizza(string id)
		{
			var pizza = await _context.Pizzas.FindAsync(id);
			if (pizza == null)
			{
				return NotFound();
			}

			_context.Pizzas.Remove(pizza);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool PizzaExists(string id)
		{
			return _context.Pizzas.Any(e => e.PizzaId == id);
		}
	}
}
