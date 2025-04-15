namespace Ecommerce_Web.Helpers
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public bool ascending { get; set; }
        public string Name { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
