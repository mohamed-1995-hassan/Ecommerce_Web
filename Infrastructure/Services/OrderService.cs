
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IDeliveryMethodRepository deliveryMethodRepository,
                            IOrderRepository orderRepository,
                            IProductRepository productRepository,
                            ICartRepository cartRepository)
        {
            _deliveryMethodRepository = deliveryMethodRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public async Task<Order> CreateOrder(string buyerEmail, int deliveryMethodId, Guid cartId, Address shippingAddress)
        {
            var cart = await _cartRepository.GetUserCart(cartId);
            var items = new List<OrderItem>();

            foreach (var item in cart.CartItems) 
            {
                var productItem = await _productRepository.GetProductByIdAsync(item.ProductId);
                var orderItem = new OrderItem
                {
                    Product = productItem,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }
            var deliveryMethod = await _deliveryMethodRepository.GetDeliveryMethodAsync(deliveryMethodId);

            var subTotal = items.Sum(i => i.Product.Price * i.Quantity);

            var order = new Order(buyerEmail, shippingAddress, items, subTotal, deliveryMethod);

            await _orderRepository.CreateOrder(order);

            await _cartRepository.RemoveCart(cartId);
            return order;
        }

        public async Task<List<DeliveryMethod>> GetDeliveryMethods()
        {
            return await _deliveryMethodRepository.GetDeliveryMethodsAsync();
        }

        public async Task<Order> GetOrderForUserByIdAsync(int id, string buyerEmail)
        {
            return await _orderRepository.GetUserOrder(id, buyerEmail);
        }

        public async Task<List<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _orderRepository.GetUserOrders(buyerEmail);
        }
    }
}
