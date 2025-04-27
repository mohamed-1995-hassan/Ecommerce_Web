
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartRepository
    {
        Task AddCartItem(int cartId, int productId, int quantity);
        Task<bool> RemoveCartItem(int cartId, int productId);
        Task<Cart> GetUserCart(int cartId);
    }
}
