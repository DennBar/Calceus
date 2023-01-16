namespace Calceus.Client.Services.SizeService
{
    public class SizeService : ISizeService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public SizeService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public List<Size> Sizes { get; set; } = new List<Size>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;


        public event Action SizeChanged;

        public async Task<string> AddSize(Size size)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/size/admin", size);

                return $"admin/sizes/{1}";
            }
            else
            {
                return "login";
            }
        }

        public async Task<ServiceResponse<Size>> GetSizeById(int sizeId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Size>>($"api/size/{sizeId}");

            return response;
        }

        public async Task GetSizes(int page)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<SizeResponse>>($"api/size/admin/all/{page}");

            if (response != null && response.Data != null)
            {
                Sizes = response.Data.Sizes;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            SizeChanged?.Invoke();
        }

        public async Task<string> UpdateSize(Size size)
        {
            if(await IsUserAuthenticated())
            {
                await _http.PutAsJsonAsync("api/size/admin", size);

                return $"admin/sizes/{1}";
            }
            else
            {
                return "login";
            }           
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
