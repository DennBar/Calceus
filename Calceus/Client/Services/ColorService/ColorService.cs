namespace Calceus.Client.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public ColorService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }
        public List<Color> AllMyColors { get; set; } = new List<Color>();
        public List<Color> MyColors { get; set; } = new List<Color>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action ColorChanged;

        public async Task<string> AddMyColor(Color color)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/color/business", color);

                return $"business/colors/{1}";
            }
            else
            {
                return "login";
            }
        }

        public async Task GetAllMyColors(int page)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<ColorResponse>>($"api/color/business/{page}");

            if (response != null && response.Data != null)
            {
                AllMyColors = response.Data.Colors;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            ColorChanged?.Invoke();
        }

        public async Task<ServiceResponse<Color>> GetColorById(int colorId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Color>>($"api/color/{colorId}");

            return response;
        }

        public async Task GetMyColors()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Color>>>("api/color");

            if (response != null && response.Data != null)
            {
                MyColors = response.Data;
            }
        }

        public async Task<string> UpdateMyColor(Color color)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PutAsJsonAsync("api/color/business", color);

                return $"business/colors/{1}";
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
