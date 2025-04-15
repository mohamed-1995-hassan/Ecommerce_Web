
using Core.Entities;
using Ecommerce_Web.Data;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductTypes.Any())
            {
                await context.ProductTypes.AddRangeAsync(GetProductTypes());
                context.SaveChanges();
            }

            if (!context.ProductBrands.Any())
            {
                await context.ProductBrands.AddRangeAsync(GetProductBrands());
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var productTypes = context.ProductTypes.ToList();
                var productBrands = context.ProductBrands.ToList();

                var products = context.Products.AddRangeAsync(new List<Product>
                {
                    new Product
                    {
                        Name = "Product one Name",
                        Description = "Product one Description",
                        PictureUrl = "image/photo.png",
                        Price = 100,
                        ProductBrandId = productBrands[0].Id,
                        ProductTypeId = productTypes[0].Id,
                    },
                    new Product
                    {
                        Name = "Product two Name",
                        Description = "Product two Description",
                        PictureUrl = "image/photo.png",
                        Price = 300,
                        ProductBrandId = productBrands[1].Id,
                        ProductTypeId = productTypes[1].Id,
                    }
                });
            }
        }

        private static List<ProductType> GetProductTypes() => new List<ProductType>
        {
            new ProductType
            {
                Name = "Books",
            },
            new ProductType
            {
                Name = "Gaming"
            }
        };

        private static List<ProductBrand> GetProductBrands() => new List<ProductBrand>
        {
            new ProductBrand
            {
                Name = "Brand1",
            },
            new ProductBrand
            {
                Name = "Brand2"
            }
        };
    }
}
