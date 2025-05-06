
using Core.Entities;
using Core.Interfaces;
using Ecommerce_Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly StoreContext _db;
        public CartRepository(StoreContext db)
        {
            _db = db;
        }
        public async Task AddCartItem(Guid cartId,int productId, int quantity)
        {
            using var transaction = _db.Database.BeginTransaction();

            
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart is null)
            {
                cart = new Cart
                {
                    Id = cartId
                };
                _db.Carts.Add(cart);
            }
            _db.SaveChanges();
            var cartItem = _db.CartItems.FirstOrDefault(c => c.CartId == cart.Id
                                                        && c.ProductId == productId);
            if (cartItem is not null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var book = _db.Products.Find(productId);
                cartItem = new CartItem
                {
                    ProductId = productId,
                    CartId = cart.Id,
                    Quantity = quantity
                };
                _db.CartItems.Add(cartItem);
            }
            _db.SaveChanges();
            transaction.Commit();
            
        }

        public async Task RemoveCartItem(Guid cartId, int productId)
        {
            var cart = await _db.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");

            var cartItem = _db.CartItems.FirstOrDefault(c => c.CartId == cart.Id
                                                        && c.ProductId == productId);
            if (cartItem is null)
               throw new InvalidOperationException("Not items in cart");

            cartItem.Quantity = cartItem.Quantity - 1;

            if (cartItem.Quantity == 0)
                _db.CartItems.Remove(cartItem);
                
            if(cart.CartItems.Count == 0)
                _db.Carts.Remove(cart);

            _db.SaveChanges();
        }

        public async Task RemoveCart(Guid cartId)
        {
            var cart = await _db.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart is null)
                throw new InvalidOperationException("Invalid cart");
            _db.Carts.Remove(cart);

            _db.SaveChanges();
        }

        public async Task<Cart> GetUserCart(Guid cartId)
        {
            
            var shoppingCart = await _db.Carts
                                        .Include(a => a.CartItems)
                                        .ThenInclude(a => a.Product)  
                                        .Include(a => a.CartItems)
                                        .ThenInclude(a => a.Product.ProductType)  
                                        .Include(a => a.CartItems)
                                        .ThenInclude(a => a.Product.ProductBrand)  
                                        .FirstOrDefaultAsync(a => a.Id == cartId);

            return shoppingCart;
        }
    }
}
