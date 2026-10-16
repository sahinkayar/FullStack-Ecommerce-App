using api.Dtos.Product;
using api.models;

namespace api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
               
            };
        }
        public static Product ToProductFromCreateDto(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };
        }

        public static void ToProductFromUpdateDto(this UpdateProductDto updateDto, Product existingProduct)
        {


            existingProduct.Name = updateDto.Name;
            existingProduct.Description = updateDto.Description;
            existingProduct.Price = updateDto.Price;
            existingProduct.Stock = updateDto.Stock;
            existingProduct.CategoryId = updateDto.CategoryId;

        }
    }
}
