namespace Calceus.Client.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _http;

        public StoreService(HttpClient http)
        {
            _http = http;
        }

        public List<StoreResponse> Stores { get; set; }
        public int SizeId { get; set; }

        public async Task GetStoreByProductIdGroupBySize(int productId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<StoreResponse>>>($"api/store/product/{productId}");

            if (response != null && response.Data != null)
            {               
                Stores = response.Data;
            }
        }
    }
}
