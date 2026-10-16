using api.Data;
using api.models;
using api.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Product;

namespace api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        public ProductController(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products =await _context.Products.ToListAsync();

            return Ok(products.Select((p)=>p.ToProductDto()));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindProduct([FromRoute] int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound("Please check entered id again!");

            return Ok(product);


        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
           var product =productDto.ToProductFromCreateDto();
            _context.Products.Add(product);
           await _context.SaveChangesAsync();
            return Ok(product.ToProductDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto updateDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound("Product not found");
                                                                 
          updateDto.ToProductFromUpdateDto(existingProduct);
            await _context.SaveChangesAsync();
            return Ok(existingProduct.ToProductDto());



        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound("please check entered id again!");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

