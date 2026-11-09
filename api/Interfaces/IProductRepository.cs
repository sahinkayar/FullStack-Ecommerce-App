using api.Dtos.Product;
using api.models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductAsync();
        public Task<Product?> FindProductByIdAsync(int id);

        public Task<Product?> CreateProductAsync(Product product);

        public Task<Product?> UpdateProductAsync(int id ,UpdateProductDto updateDto);
        public Task<Product?> RemoveProductAsync(int id);


    }
}
