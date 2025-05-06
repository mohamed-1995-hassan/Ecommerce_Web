using Core.Entities;
using Core.Entities.identity;
using Core.Entities.OrderAggregate;
using Ecommerce_Web.Dtos;

namespace Ecommerce_Web
{
    public static class Mapper
    {
        public static ProductToReturnDto ToProductDto(this Product product,
                                                      IConfiguration config)
        {
            return new ProductToReturnDto
            {
                Price = product.Price,
                PictureUrl = config["ApiUrl"] + product.PictureUrl,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name,
            };
        }

        public static AddressDto ToAddressDto(this Core.Entities.identity.Address address)
        {
            return new AddressDto
            {
                City = address.City,
                FirstName = address.FirstName,
                LastName = address.LastName,
                State = address.State,
                Street = address.Street,
                Zip = address.Zip   
            };
        }

        public static OrderToReturnDto ToOrderToReturn(this Order order, IConfiguration config)
        {
            return new OrderToReturnDto
            {
                Id = order.Id,
                Address = order.Address,
                BuyerEmail = order.BuyerEmail,
                DeliveryMethod = order.DeliveryMethod.ShortName,
                OrderDate = order.OrderDate,
                ShippingPrice = order.DeliveryMethod.Price,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                Total = order.GetTotal,
                OrderItems = order.OrderItems.Select(o => o.ToOrderItemDto(config)).ToList()
            };
        }
        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem, IConfiguration config)
        {
            return new OrderItemDto
            {
                PictureUrl = config["ApiUrl"] + orderItem.Product.PictureUrl,
                Price = orderItem.Product.Price,
                ProductId = orderItem.Product.Id,
                ProductName = orderItem.Product.Name,
                Quantity = orderItem.Quantity
            };
        }
    }
}
