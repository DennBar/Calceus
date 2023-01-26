namespace Calceus.Server.Services.StoreService
{
    public interface IStoreService
    {
        Task<ServiceResponse<List<StoreResponse>>> GetStoreByProductIdGroupBySize(int productId);
    }
}
