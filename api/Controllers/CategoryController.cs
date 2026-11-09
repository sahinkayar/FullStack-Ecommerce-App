using api.Data;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    [Authorize]
    public class CategoryController : ControllerBase
    {


        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {

            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetCategoriesAsync();
            return Ok(categories.Select((c) => c.ToCategoryDto()));

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> FindCategory([FromRoute] int id)
        {
            var category = await _categoryRepo.FindCategorytByIdAsync(id);
            if (category == null) return NotFound("Please check entered id again!");

            return Ok(category.ToCategoryDto());

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var category = categoryDto.ToCategoryFromCreate();
            await _categoryRepo.CreateCategoryAsync(category);
            return Ok(category);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateDto)
        {
            var existingCategory = await _categoryRepo.UpdateCategoryAsync(id, updateDto);
            if (existingCategory == null) return NotFound("Could'nt match any categories");

            return Ok(existingCategory.ToCategoryDto());
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryRepo.RemoveCategoryAsync(id);
            return NoContent();
        }
    }
}
