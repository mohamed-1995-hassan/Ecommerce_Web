
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        Task<DeliveryMethod> GetDeliveryMethodAsync(int id);
    }
}
