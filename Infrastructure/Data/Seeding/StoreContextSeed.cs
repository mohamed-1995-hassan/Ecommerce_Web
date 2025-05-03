
using Core.Entities;
using Core.Entities.OrderAggregate;
using Ecommerce_Web.Data;

namespace Infrastructure.Data.Seeding
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
                    },
                    new Product
                    {
                        Name = "Product Three Name",
                        Description = "Product Three Description",
                        PictureUrl = "image/photo.png",
                        Price = 100,
                        ProductBrandId = productBrands[0].Id,
                        ProductTypeId = productTypes[0].Id,
                    },
                    new Product
                    {
                        Name = "Product Four Name",
                        Description = "Product Four Description",
                        PictureUrl = "image/photo.png",
                        Price = 300,
                        ProductBrandId = productBrands[1].Id,
                        ProductTypeId = productTypes[1].Id,
                    },
                    new Product
                    {
                        Name = "Product Five Name",
                        Description = "Product Five Description",
                        PictureUrl = "image/photo.png",
                        Price = 100,
                        ProductBrandId = productBrands[0].Id,
                        ProductTypeId = productTypes[0].Id,
                    }
                });

                context.SaveChanges();
            }

            if (!context.DeliveryMethods.Any())
            {
                await context.DeliveryMethods.AddRangeAsync(GetDeliveryMethods());
                context.SaveChanges();
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

        private static List<DeliveryMethod> GetDeliveryMethods() => new List<DeliveryMethod>
        {
            new DeliveryMethod
            {
                ShortName = "UPS1",
                Description = "Fastest delivery time",
                DeliveryTime = "1-2 Days",
                Price = 10.00
            },
            new DeliveryMethod
            {
                ShortName = "UPS2",
                Description = "Get it within 5 days",
                DeliveryTime = "2-5 Days",
                Price = 5.00
            },
            new DeliveryMethod
            {
                ShortName = "UPS3",
                Description = "Slower but cheap",
                DeliveryTime = "5-10 Days",
                Price = 2.00
            },
            new DeliveryMethod
            {
                ShortName = "FREE",
                Description = "Free! You get what you pay for",
                DeliveryTime = "1-2 Weeks",
                Price = 0.00
            }
        };
    }
}
