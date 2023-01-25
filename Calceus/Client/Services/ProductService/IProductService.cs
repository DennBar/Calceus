namespace Calceus.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        List<Product> MyProducts { get; set; }
        List<Product> Products { get; set; }
        int PageIndex { get; set; }
        int PageCount { get; set; }
        Task GetMyProducts(int page);
        Task<ServiceResponse<Product>> GetProductById(int productId);
        Task GetAllProducts(string? categoryUrl = null);
        Task<string> AddProduct(Product product);
        Task<string> UpdateProduct(Product product);
        Task<string> UpsertMyStoreByProduct(Product product);
    }
}
