using api.Data;
using api.Dtos.Product;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProductRepository : api.Interfaces.IProductRepository
    {
        private readonly ECommerceDbContext _context;
        public ProductRepository(ECommerceDbContext context)
        {
            _context=context;
        }

        public async Task<List<Product>> GetProductAsync()
        {
           return  await _context.Products.ToListAsync();
        }

        public async Task<Product?> FindProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync((f)=>f.Id==id);
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id ,UpdateProductDto updateDto)
        {
            var existingProduct = await FindProductByIdAsync(id);
            if (existingProduct == null) return null;

            updateDto.ToProductFromUpdateDto(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        

        public async Task<Product?> RemoveProductAsync(int id)
        {
            var product = await FindProductByIdAsync(id);
            if (product == null) return null;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
