using api.Dtos.Category;
using api.models;

namespace api.Mappers
{
    public static class CategoryMapper
    {

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => p.ToProductDto()).ToList()
            };
        }

        public static Category ToCategoryFromCreate(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
               
               
            };
        }
        public static void ToCategoryFromUpdate(this UpdateCategoryDto updateDto,Category existingCategory)
        {
            {
                existingCategory.Name = updateDto.Name;
            }
        }
    }
}


