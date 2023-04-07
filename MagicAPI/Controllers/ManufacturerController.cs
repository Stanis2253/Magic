using MagicDataAccess.Data;
using MagicModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ManufacturerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Category/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturer()
        {
            var manufacturer = _context.Manufacturer;
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(await manufacturer.ToListAsync());
        }
        //GET: api/Category/5
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
        {
            _context.Add(manufacturer);
            await _context.SaveChangesAsync();

            return Ok(manufacturer);
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return BadRequest();
            }
            //_dbContext.Entry(manufacturer).State = EntityState.Modified;

            _context.Manufacturer.Update(manufacturer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var manufacturer = await _context.Manufacturer.FindAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }
            _context.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}

