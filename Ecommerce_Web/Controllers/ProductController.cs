using Core;
using Core.Entities;
using Core.Interfaces;
using Ecommerce_Web.Dtos;
using Ecommerce_Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _config;
        public ProductController(IProductRepository productRepository, IConfiguration config)
        {
            _productRepository = productRepository;
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(ProductQueryParams queryParams)
        {
            var products = await _productRepository.GetProductsAsync(queryParams.BrandId,
                                                                     queryParams.TypeId,
                                                                     queryParams.Name,
                                                                     queryParams.ascending,
                                                                     queryParams.PageIndex,
                                                                     queryParams.PageSize);

            var count = await _productRepository.GetCountAsync(queryParams.BrandId,
                                                               queryParams.TypeId,
                                                               queryParams.Name);

            var productsDto = products.Select(p => p.ToProductDto(_config)).ToList();

            var result = new Pagination<ProductToReturnDto>
            {
                Data = productsDto,
                Count = count,
                PageIndex = queryParams.PageIndex,
                PageSize = queryParams.PageSize
            };

            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product.ToProductDto(_config);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productRepository.GetProductBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            return Ok(await _productRepository.GetProductTypesAsync());
        }
    }
}
