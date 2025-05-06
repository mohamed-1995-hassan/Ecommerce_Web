
using Core;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Ecommerce_Web.Dtos;
using Ecommerce_Web.Errors;
using Ecommerce_Web.Extentions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _config;
        public OrderController(IOrderService orderService, IConfiguration config, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _config = config;
            _orderRepository = orderRepository;
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
        [HttpGet]
        public async Task<ActionResult<Pagination<OrderToReturnDto>>> GetOrdersForUser(int pageIndex, int pageSize)
        {
            var email = User.RetrieveEmailFromPrincipl();
            var orders = (await _orderRepository.GetUserOrders(email, pageIndex, pageSize))
                .Select(o => o.ToOrderToReturn(_config)).ToList();

            var count = await _orderRepository.GetCountAsync(email);

            var result = new Pagination<OrderToReturnDto>
            {
                Count = count,
                Data = orders,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUserById(int id)
        {
            var email = User.RetrieveEmailFromPrincipl();
            var order = await _orderService.GetOrderForUserByIdAsync(id, email);
            if (order == null) return BadRequest(new ApiResponse(404));
            return Ok(order.ToOrderToReturn(_config));
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<List<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethods());
        }
    }
}
