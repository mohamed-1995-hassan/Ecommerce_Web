
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        Task<List<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<DeliveryMethod> GetDeliveryMethodAsync(int deliveryMethodId);
    }
}
