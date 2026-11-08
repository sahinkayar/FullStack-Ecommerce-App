using api.models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrdersAsync(string userId);
        Task<Order?> CreateOrderFromCartAsync(string userId);
    }
}
