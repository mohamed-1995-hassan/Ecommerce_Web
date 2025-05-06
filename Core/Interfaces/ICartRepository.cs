
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartRepository
    {
        Task AddCartItem(Guid cartId, int productId, int quantity);
        Task RemoveCartItem(Guid cartId, int productId);
        Task<Cart> GetUserCart(Guid cartId);
        Task RemoveCart(Guid cartId);
    }
}
