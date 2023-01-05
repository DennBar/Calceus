namespace Calceus.Client.Services.SizeService
{
    public class SizeService : ISizeService
    {
        private readonly HttpClient _http;

        public SizeService(HttpClient http)
        {
            _http = http;
        }

        public List<Size> Sizes { get; set; } = new List<Size>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;


        public event Action SizeChanged;

        public async Task<Size> AddSize(Size size)
        {
            var response = await _http.PostAsJsonAsync("api/size", size);

            var newSize = (await response.Content.ReadFromJsonAsync<ServiceResponse<Size>>()).Data;

            return newSize;
        }

        public async Task<ServiceResponse<Size>> GetSizeById(int sizeId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Size>>($"api/product{sizeId}");

            return response;
        }

        public async Task GetSizes(int page)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<SizeResponse>>($"api/size/all/{page}");

            if (response != null && response.Data != null)
            {
                Sizes = response.Data.Sizes;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            SizeChanged?.Invoke();
        }

        public async Task<Size> UpdateSize(Size size)
        {
            var response = await _http.PutAsJsonAsync("api/size", size);

            var updatedSize = (await response.Content.ReadFromJsonAsync<ServiceResponse<Size>>()).Data;

            return updatedSize;
        }
    }
}
