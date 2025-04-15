
using Core.Entities;
using Core.Interfaces;
using Ecommerce_Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _storeContext
                                      .Products
                                      .Include(p => p.ProductBrand)
                                      .Include(p => p.ProductType)
                                      .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync(int? brandId = null,
                                                          int? typeId = null,
                                                          string name = "",
                                                          bool ascending = true,
                                                          int pageIndex = 1,
                                                          int pageSize = 5)
        {
            
            var query = _storeContext
                                     .Products
                                     .Include(p => p.ProductBrand)
                                     .Include(p => p.ProductType)
                                     .Where(GetPredicate(brandId, typeId, name));


            query = ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var data = await query.ToListAsync();

            return data;
        }
        public async Task<int> GetCountAsync(int? brandId = null,
                                             int? typeId = null,
                                             string name = "")
        {
            var query = _storeContext.Products.Where(GetPredicate(brandId, typeId, name));
            return await query.CountAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        private Expression<Func<Product, bool>> GetPredicate(int? brandId = null, int? typeId = null, string name = "") =>
                                            p => (!brandId.HasValue || p.ProductBrandId == brandId) &&
                                                 (!typeId.HasValue || p.ProductTypeId == typeId) &&
                                                 (!string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name));
    }
}
