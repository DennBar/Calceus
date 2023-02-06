using Blazored.LocalStorage;
using Calceus.Shared;

namespace Calceus.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public event Action CartChanged;

        public async Task AddToCart(CartItem cart)
        {
            var myCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (myCart == null)
            {
                myCart = new List<CartItem>();
            }

            myCart.Add(cart);

            await _localStorage.SetItemAsync("cart", myCart);
            CartChanged.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

        public async Task<List<CartResponse>> GetCartProducts()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            var response = await _http.PostAsJsonAsync("api/cart/products", cart);

            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartResponse>>>();

            return cartProducts.Data;
        }
    }
}
