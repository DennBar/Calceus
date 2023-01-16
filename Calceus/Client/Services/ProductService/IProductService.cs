namespace Calceus.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        List<Product> MyProducts { get; set; }
        int PageIndex { get; set; }
        int PageCount { get; set; }
        Task GetMyProducts(int page);
        Task<ServiceResponse<Product>> GetMyProductById(int productId);
        Task<string> AddProduct(Product product);
        Task<string> UpdateProduct(Product product);
    }
}
