using MagicDataAccess.Data;
using MagicModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace MagicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PriceController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/Price/5
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(Price price)
        {
            _context.Add(price);
            await _context.SaveChangesAsync();

            return Ok(price);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeletePrice(int id)
        {

            var price = await _context.Price.FindAsync(id);

            if (price == null)
            {
                return NotFound();
            }
            _context.Remove(price);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
