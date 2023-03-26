using MagicDataAccess.Data;
using MagicModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    

    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Category/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (_dbContext.Category == null)
            {
                return NotFound();
            }
            return await _dbContext.Category.ToListAsync();
        }
        //GET: api/Category/5
        [HttpGet("id")]
        public async Task<ActionResult<Category>> GetCategory(int Id)
        {
            if (_dbContext.Category == null)
            {
                return NotFound();
            }

            var category = await _dbContext.Category.FindAsync(Id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        //GET: api/Category/5
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _dbContext.Category.Add(category);
            await _dbContext.SaveChangesAsync();

            return Ok(category);
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_dbContext.Category == null)
            {
                return NotFound();
            }
            var category = await _dbContext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(long id)
        {
            return (_dbContext.Category?.Any(c => c.Id == id)).GetValueOrDefault();
        }

    }
}
