namespace Calceus.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public ProductService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public List<Product> MyProducts { get; set; } = new List<Product>();
        public List<Product> Products { get; set; } = new List<Product>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action ProductChanged;

        public async Task<string> AddProduct(Product product)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/product/business", product);

                return $"business/products/{1}";
            }
            else
            {
                return "login";
            }
        }

        public async Task GetAllProducts(string? categoryUrl = null)
        {
            var response = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (response != null && response.Data != null)
            {
                Products = response.Data;
            }

            ProductChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetMyProductById(int productId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");

            return response;
        }

        public async Task GetMyProducts(int page)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<ProductResponse>>($"api/product/business/me/{page}");

            if (response != null && response.Data != null)
            {
                MyProducts = response.Data.Products;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            ProductChanged?.Invoke();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PutAsJsonAsync("api/product/business", product);

                return $"business/products/{1}";
            }
            else
            {
                return "login";
            }
        }

        public async Task<string> UpsertMyStoreByProduct(Product product)
        {
            if (await IsUserAuthenticated())
            {
                await _http.PutAsJsonAsync("api/product/business/store", product);

                return $"business/stores/{1}";
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
