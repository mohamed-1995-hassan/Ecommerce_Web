
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Ecommerce_Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly StoreContext _db;
        public DeliveryMethodRepository(StoreContext db)
        {
            _db = db;
        }
        public async Task<DeliveryMethod> GetDeliveryMethodAsync(int id)
        {
            return await _db.DeliveryMethods.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
