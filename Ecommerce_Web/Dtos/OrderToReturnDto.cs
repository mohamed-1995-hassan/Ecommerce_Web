using Core.Entities.OrderAggregate;

namespace Ecommerce_Web.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }
        public double ShippingPrice { get; set; }
    }
}
