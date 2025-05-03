
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Ecommerce_Web.Dtos;
using Ecommerce_Web.Errors;
using Ecommerce_Web.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipl();

            var address = new Address
            {
                City = orderDto.Address.City,
                FirstName = orderDto.Address.FirstName,
                LastName = orderDto.Address.LastName,
                State = orderDto.Address.State,
                Street = orderDto.Address.Street,
                Zip = orderDto.Address.Zip
            };

            var order = await _orderService.CreateOrder(email,
                                                        orderDto.DeliveryMethodId,
                                                        orderDto.CartId,
                                                        address);
            if(order == null)
                return BadRequest(new ApiResponse(400, "Problem Creating Order"));

            return Ok(order);
        }
    }
}
