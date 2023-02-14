namespace Calceus.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        public OrderService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        public async Task<OrderResponse> GetCustomerOrderDetails(int orderId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<OrderResponse>>($"api/order/customer/{orderId}");

            return response.Data;
        }

        public async Task<List<OrderCustomerResponse>> GetCustomerOrders()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<OrderCustomerResponse>>>("api/order/customer");

            return response.Data;
        }

        public async Task<List<OrderVendorResponse>> GetVendorOrders()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<OrderVendorResponse>>>("api/order/business");

            return response.Data;
        }

        public async Task<string> PlaceOrder()
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsync("api/order/customer", null);
                return "orders";
            }
            else
            {
                return "login";
            }
        }
    }
}
