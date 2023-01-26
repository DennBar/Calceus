using MudBlazor;

namespace Calceus.Client.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _http;

        public StoreService(HttpClient http)
        {
            _http = http;
        }

        public int SizeId { get; set; }
        public IEnumerable<Store> Stores { get; set; }

        public async Task GetStoreByProductIdGroupBySize(int productId)
        {
            var response = await _http.GetFromJsonAsync<StoreResponse>($"api/store/product/{productId}");

            if (response != null)
            {
                SizeId = response.SizeId;
                Stores = response.Stores;
            }
        }
    }
}
