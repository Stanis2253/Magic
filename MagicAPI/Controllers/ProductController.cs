using AutoMapper;
using MagicDataAccess.Data;
using MagicModel.DTO;
using MagicModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace MagicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Product/
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProductDTO(int? IdCategory, int? IdManufacturer)
        {

            var product = _context.Product;

            List<ProductDTO> productsList = new List<ProductDTO>();

            if (product == null)
            {
                return NotFound();
            }

                foreach (Product prod in product)
                {
                    if (IdCategory != null || IdManufacturer != null)
                    {
                        if (IdCategory != null)
                        {
                            if (prod.Category.Id != IdCategory)
                            {
                                continue;
                            }
                        }
                        if (IdManufacturer != null)
                        {
                            if (prod.Manufacturer.Id != IdManufacturer)
                            {
                                continue;
                            }
                        }

                    }
                    var price = _context.Price;
                    //var image = _context.Images.Where(predicate => predicate.ProductId == prod.IdProduct).ToList();

                    ProductDTO productDTO = new ProductDTO
                    {
                        IdProduct = prod.IdProduct,
                        Name = prod.Name,
                        Description = prod.Description,
                        ShortDesc = prod.ShortDesc,
                        ManufacturerId = prod.ManufacturerId,
                        CategoryId = prod.CategoryId,
                    };
                
                    productsList.Add(productDTO);
                }
            


            if (productsList == null)
            {
                return NotFound();
            }


            return Ok(productsList);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct
            (string name, string description, string shorDesc, int manufacturerId, int categoryId)
        {

            Product product = new Product
            {
                Name = name,
                Description = description,
                ShortDesc = shorDesc,
                ManufacturerId = manufacturerId,
                Manufacturer = _context.Manufacturer.Find(manufacturerId),
                Category = _context.Category.Find(categoryId),
            };

            _context.Product.Add(product);

            await _context.SaveChangesAsync();

            return Ok(product);
        }
        [HttpPut("id")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.IdProduct)
            {
                return BadRequest();
            }
            //_dbContext.Entry(product).State = EntityState.Modified;
            _context.Update(product);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var img = _context.Images.Where(p => p.ProductId == id);

            if (img != null)
            {
                _context.Images.RemoveRange(img);
            }

            var price = _context.Price.Where(p => p.ProductId == id);

            if (price != null)
            {
                _context.Price.RemoveRange(price);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
