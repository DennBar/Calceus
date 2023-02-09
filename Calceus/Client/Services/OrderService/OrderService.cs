using Microsoft.AspNetCore.Components;

namespace Calceus.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly NavigationManager _navigationManager;
        public OrderService(HttpClient http, IAuthService authService, NavigationManager navigationManager)
        {
            _http = http;
            _authService = authService;
            _navigationManager = navigationManager;

        }
        public async Task PlaceOrder()
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }
    }
}
