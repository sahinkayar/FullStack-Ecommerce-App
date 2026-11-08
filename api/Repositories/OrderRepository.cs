using api.Data;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context)
        {

            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync(string userId)
        {
            return await _context.Orders.Include((o) => o.OrderItems).ThenInclude((ci) => ci.Product).Where((u) => u.AppUserId == userId).OrderByDescending((o) => o.CreatedAt).ToListAsync();

        }
        public async Task<Order?> CreateOrderFromCartAsync(string userId)
        {
            var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.AppUserId == userId);
            if (cart == null || cart.CartItems.Count == 0) return null;

            foreach (var ci in cart.CartItems)
            {
                if (ci.Product!.Stock < ci.Quantity) return null;
            }
            var order = new Order { AppUserId = userId };

            foreach (var ci in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = ci.ProductId,
                    Product = ci.Product,
                    Quantity = ci.Quantity,
                    Price = ci.Product!.Price
                });
                    ci.Product.Stock -= ci.Quantity;
            }
            order.TotalPrice = order.OrderItems.Sum((oi) => oi.Price * oi.Quantity);
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();
            return order;


        }
    }
}
