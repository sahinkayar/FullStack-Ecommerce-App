using api.Data;
using api.Dtos.Category;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoryController :ControllerBase
    {

        private readonly ECommerceDbContext _context;
        public CategoryController(ECommerceDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories
          .Include(c => c.Products)  
          .ToListAsync();
            return Ok(categories.Select((c) => c.ToCategoryDto()));
           
        }

        [HttpPost]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var category = categoryDto.ToCategoryFromCreate();
             _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category); 
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id , [FromBody] UpdateCategoryDto updateDto)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) return NotFound("Category not found");
            updateDto.ToCategoryFromUpdate(existingCategory);
            await _context.SaveChangesAsync();
            return Ok(existingCategory.ToCategoryDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound("Category not found");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
