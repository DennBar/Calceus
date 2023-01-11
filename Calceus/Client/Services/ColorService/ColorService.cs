namespace Calceus.Client.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly HttpClient _http;

        public ColorService(HttpClient http)
        {
            _http = http;
        }

        public event Action ColorChanged;
        public List<Color> AllMyColors { get; set; } = new List<Color>();
        public List<Color> MyColors { get; set; } = new List<Color>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public async Task<Color> AddMyColor(Color color)
        {
            var response = await _http.PostAsJsonAsync("api/color/business", color);

            var newColor = (await response.Content.ReadFromJsonAsync<ServiceResponse<Color>>()).Data;

            return newColor;
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

        public async Task<Color> UpdateMyColor(Color color)
        {
            var response = await _http.PutAsJsonAsync("api/color/business", color);

            var updatedColor = (await response.Content.ReadFromJsonAsync<ServiceResponse<Color>>()).Data;

            return updatedColor;
        }
    }
}
