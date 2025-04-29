
namespace Core.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
