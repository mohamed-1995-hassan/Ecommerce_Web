
namespace Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<CartItem> CartItems { get; set; }
    }
}
