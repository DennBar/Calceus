namespace Calceus.Server.Services.StoreService
{
    public interface IStoreService
    {
        Task<ServiceResponse<StoreResponse>> GetMyStoreProducts(int page);
        Task<ServiceResponse<Store>>GetMyStoreProductById
    }
}
