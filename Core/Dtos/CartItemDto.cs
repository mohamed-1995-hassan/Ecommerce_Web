
namespace Core.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string brand { get; set; }
        public string type { get; set; }
    }
}
