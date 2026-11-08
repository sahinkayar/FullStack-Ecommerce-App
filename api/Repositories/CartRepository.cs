using api.Data;
using api.Dtos.Cart;
using api.Interfaces;
using api.models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _context;
        public CartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartAsync(string userId)
        {
            return await _context.Carts.Include(c => c.CartItems).ThenInclude((ci) => ci.Product).FirstOrDefaultAsync((c) => c.AppUserId == userId);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> AddItemAsync(string userId, int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            var cart = await GetCartAsync(userId);
            if (cart == null)
            {
                cart = new Cart { AppUserId = userId };
                _context.Carts.Add(cart);
            }

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task<Cart?> UpdateCartAsync(string userId, int productId, int quantity)
        {
            var cart = await GetCartAsync(userId);

            var item = cart?.CartItems.FirstOrDefault((c) => c.ProductId == productId);
            if (cart == null || item == null) return null;

            item.Quantity = quantity;

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> DeleteItemAsync(string userId, int productId)
        {
            var cart = await GetCartAsync(userId);

            var item = cart?.CartItems.FirstOrDefault((c) => c.ProductId == productId);
            if (cart == null || item == null) return null;
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}
