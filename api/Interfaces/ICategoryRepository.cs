using api.Dtos.Category;
using api.Dtos.Product;
using api.models;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<Category?> FindCategorytByIdAsync(int id);

        public Task<Category?> CreateCategoryAsync(Category category);

        public Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto);
        public Task<Category?> RemoveCategoryAsync(int id);
    }
}
