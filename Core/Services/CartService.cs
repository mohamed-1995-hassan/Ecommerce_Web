
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

        public async Task<CartDto> GetUserCart(Guid cartId)
        {
            var cart = await _cartRepository.GetUserCart(cartId);
            var cartDto = new CartDto
            {
                Id = cart.Id,
                Items = cart.CartItems.Select(c => new CartItemDto
                {
                    Id = c.ProductId,
                    brand = c.Product.ProductBrand.Name,
                    ProductName = c.Product.Name,
                    Price = c.Product.Price,
                    PictureUrl = _config["ApiUrl"] + c.Product.PictureUrl,
                    Quantity = c.Quantity,
                    type = c.Product.ProductType.Name
                    
                }).ToList()
            };
            return cartDto;
        }

        public async Task AddCartItem(Guid cartId, int productId, int quantity)
        {
            await _cartRepository.AddCartItem(cartId, productId, quantity);
        }
        public async Task RemoveCartItem(Guid cartId, int productId)
        {
            await _cartRepository.RemoveCartItem(cartId, productId);
        }
    }
}
