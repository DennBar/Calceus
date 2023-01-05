namespace Calceus.Client.Services.SizeService
{
    public class SizeService : ISizeService
    {
        private readonly HttpClient _hhtp;

        public SizeService(HttpClient hhtp)
        {
            _hhtp = hhtp;
        }

        public List<Size> Sizes { get; set; } = new List<Size>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;


        public event Action SizeChanged;

        public async Task GetSizes(int page)
        {
            var response = await _hhtp.GetFromJsonAsync<ServiceResponse<SizeResponse>>($"api/size/all/{page}");

            if (response != null && response.Data != null)
            {
                Sizes = response.Data.Sizes;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            SizeChanged?.Invoke();
        }
    }
}
