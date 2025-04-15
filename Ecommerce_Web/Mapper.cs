using Core.Entities;
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
    }
}
