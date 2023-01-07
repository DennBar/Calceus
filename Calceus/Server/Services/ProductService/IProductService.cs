namespace Calceus.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductResponse>> GetMyProducts(int page);
        Task<ServiceResponse<Product>> GetMyProductById(int productId);
        Task<ServiceResponse<Product>> AddProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
    }
}
