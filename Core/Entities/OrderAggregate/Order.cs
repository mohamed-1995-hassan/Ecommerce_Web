
namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail,
                     Address address,
                     List<OrderItem> orderItems,
                     double subTotal,
                     DeliveryMethod deliveryMethod)
        {
            BuyerEmail = buyerEmail;
            Address = address;
            OrderItems = orderItems;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
        }
        public Order()
        {
            
        }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address Address { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }

        public double GetTotal => SubTotal + DeliveryMethod.Price;
    }
}
