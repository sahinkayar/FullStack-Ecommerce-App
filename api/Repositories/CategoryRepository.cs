using api.Data;
using api.Dtos.Category;
using api.Interfaces;
using api.Mappers;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _context;
        public CategoryRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.Include((p) => p.Products).ToListAsync();
        }
        public async Task<Category?> FindCategorytByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto)
        {
            var existingCategory = await FindCategorytByIdAsync(id);
            if (existingCategory == null) return null;

            updateDto.ToCategoryFromUpdate(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;

        }

        public async Task<Category?> RemoveCategoryAsync(int id)
        {

            var category = await FindCategorytByIdAsync(id);
            if (category == null) return null;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }



    }
}
