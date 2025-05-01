using Core.Entities;
using Core.Entities.identity;
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

        public static AddressDto ToAddressDto(this Address address)
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
    }
}
