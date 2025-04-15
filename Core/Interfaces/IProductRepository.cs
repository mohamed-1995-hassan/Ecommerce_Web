
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> GetProductsAsync(int? brandId = null,
                                                          int? typeId = null,
                                                          string name = "",
                                                          bool ascending = true,
                                                          int pageIndex = 1,
                                                          int pageSize = 5);
        Task<int> GetCountAsync(int? brandId = null,
                                int? typeId = null,
                                string name = "");
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
