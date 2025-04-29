namespace Ecommerce_Web.Dtos
{
    public class AddItemCommand
    {
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
