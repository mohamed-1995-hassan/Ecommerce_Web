using Core.Entities;
using Core.Services;
using Ecommerce_Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] AddItemCommand command) 
        {
            await _cartService.AddCartItem(command.CartId, command.ProductId, command.Quantity);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart([FromQuery] Guid cartId)
        {
            var userCart = await _cartService.GetUserCart(cartId);
            return Ok(userCart);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCartItem([FromQuery] Guid cartId, [FromQuery] int productId)
        {
            await _cartService.RemoveCartItem(cartId, productId);
            return Ok();
        }
    }
}
