using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaSalesApi.Models;

namespace PizzaSalesApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PizzaTypeController : ControllerBase
  {
    private readonly PizzaSalesContext _context;

    public PizzaTypeController(PizzaSalesContext context)
    {
      _context = context;
    }

    // GET: api/PizzaType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzaType>>> GetPizzaTypes()
    {
      return await _context.PizzaTypes.ToListAsync();
    }

    // GET: api/PizzaType/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PizzaType>> GetPizzaType(string id)
    {
      var pizzaType = await _context.PizzaTypes.FindAsync(id);

      if (pizzaType == null)
      {
        return NotFound();
      }

      return pizzaType;
    }

    // PUT: api/PizzaType/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPizzaType(string id, PizzaType pizzaType)
    {
      if (id != pizzaType.PizzaTypeId)
      {
        return BadRequest();
      }

      _context.Entry(pizzaType).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PizzaTypeExists(id))
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

    // POST: api/PizzaType
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PizzaType>> PostPizzaType(PizzaType pizzaType)
    {
      _context.PizzaTypes.Add(pizzaType);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (PizzaTypeExists(pizzaType.PizzaTypeId))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetPizzaType", new { id = pizzaType.PizzaTypeId }, pizzaType);
    }

    // DELETE: api/PizzaType/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePizzaType(string id)
    {
      var pizzaType = await _context.PizzaTypes.FindAsync(id);
      if (pizzaType == null)
      {
        return NotFound();
      }

      _context.PizzaTypes.Remove(pizzaType);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool PizzaTypeExists(string id)
    {
      return _context.PizzaTypes.Any(e => e.PizzaTypeId == id);
    }
  }
}
