
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order> GetUserOrder(int id, string userEmail);
        Task<List<Order>> GetUserOrders(string userEmail, int pageIndex, int pageSize);
        Task<int> GetCountAsync(string userEmail);
    }
}
