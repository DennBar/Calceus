namespace Calceus.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductResponse>> GetMyProducts(int page);
        Task<ServiceResponse<Product>> GetProductById(int productId);
        Task<ServiceResponse<Product>> AddProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        Task<ServiceResponse<Product>> UpsertMyStoreByProduct(Product product);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<List<Product>>> GetAllProducts();
    }
}
