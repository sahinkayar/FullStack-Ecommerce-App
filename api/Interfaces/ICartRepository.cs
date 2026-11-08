
using api.models;

namespace api.Interfaces
{
    public interface ICartRepository
    {

        public Task<Cart?> GetCartAsync(string userId);
        Task<Cart?> AddItemAsync(string userId, int productId, int quantity);
        public Task<Cart?> UpdateCartAsync(string userId,int productId,int quantity);
        public Task<Cart?> DeleteItemAsync(string userId, int productId);
        Task SaveAsync();
    }
}
