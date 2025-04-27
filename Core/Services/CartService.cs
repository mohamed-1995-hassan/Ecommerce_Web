
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IConfiguration _config;
        public CartService(ICartRepository cartRepository, IConfiguration config)
        {
            _cartRepository = cartRepository;
            _config = config;
        }

        public async Task<CartDto> GetUserCart(int cartId)
        {
            var cart = await _cartRepository.GetUserCart(cartId);
            var cartDto = new CartDto
            {
                Id = cart.Id,
                Items = cart.CartItems.Select(c => new CartItemDto
                {
                    Id = c.Id,
                    brand = c.Product.ProductBrand.Name,
                    ProductName = c.Product.Name,
                    Price = c.Product.Price,
                    PictureUrl = _config["ApiUrl"] + c.Product.PictureUrl,
                    Quantity = c.Quantity,
                    type = c.Product.ProductType.Name
                    
                })
            };
            return cartDto;
        }

        public async Task AddCartItem(int cartId, int productId, int quantity)
        {
            await _cartRepository.AddCartItem(cartId, productId, quantity);
        }
        public async Task<bool> RemoveCartItem(int cartId, int productId)
        {
            return await _cartRepository.RemoveCartItem(cartId, productId);
        }
    }
}
