namespace Calceus.Client.Services.StoreService
{
    public interface IStoreService
    {
        int SizeId { get; set; }
        List<StoreResponse> Stores { get; set; }
        Task GetStoreByProductIdGroupBySize(int productId);
    }
}
