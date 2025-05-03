
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string buyerEmail,
                                int deliveryMethodId,
                                Guid cartId,
                                Address shippingAddress);
        Task<List<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderForUserByIdAsync(int id,string buyerEmail);
        Task<List<DeliveryMethod>> GetDeliveryMethods();
    }
}
