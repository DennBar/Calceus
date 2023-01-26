namespace Calceus.Client.Services.StoreService
{
    public interface IStoreService
    {
        IEnumerable<Store> Stores { get; set; }
        int SizeId { get; set; }
        Task GetStoreByProductIdGroupBySize(int productId);
    }
}
