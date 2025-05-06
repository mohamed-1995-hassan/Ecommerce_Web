
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Ecommerce_Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _db;
        public OrderRepository(StoreContext db)
        {
            _db = db;
        }
        public async Task CreateOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Order>> GetUserOrders(string userEmail, int pageIndex, int pageSize)
        {
           return await _db.Orders
                            .Where(o => o.BuyerEmail == userEmail)
                            .Include(o => o.OrderItems)
                            .ThenInclude(o => o.Product)
                            .Include(o => o.Address)
                            .Include(o => o.DeliveryMethod)
                            .OrderByDescending(o => o.OrderDate)
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }

        public async Task<int> GetCountAsync(string userEmail)
        {
            var query = _db.Orders.Where(o => o.BuyerEmail == userEmail);
            return await query.CountAsync();
        }

        public async Task<Order> GetUserOrder(int id, string userEmail)
        {
            return await _db.Orders
                            .Include(o => o.OrderItems)
                            .ThenInclude(o => o.Product)
                            .Include(o => o.Address)
                            .Include(o => o.DeliveryMethod)
                            .FirstOrDefaultAsync(o => o.Id == id && o.BuyerEmail == userEmail);
        }
    }
}
