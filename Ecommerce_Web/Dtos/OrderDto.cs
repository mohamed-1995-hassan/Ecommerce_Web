namespace Ecommerce_Web.Dtos
{
    public class OrderDto
    {
        public Guid CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; }
    }
}
