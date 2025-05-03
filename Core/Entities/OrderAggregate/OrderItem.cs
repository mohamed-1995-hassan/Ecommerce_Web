
namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public OrderItem()
        {
            
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
