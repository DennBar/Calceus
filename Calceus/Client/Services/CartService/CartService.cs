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

        public async Task AddToCart(Cart cart)
        {
            var myCart = await _localStorage.GetItemAsync<List<Cart>>("cart");

            if (myCart == null)
            {
                myCart = new List<Cart>();
            }

            myCart.Add(cart);

            await _localStorage.SetItemAsync("cart", myCart);
            CartChanged.Invoke();
        }

        public async Task<List<Cart>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");

            if (cart == null)
            {
                cart = new List<Cart>();
            }

            return cart;
        }

        public async Task<List<CartResponse>> GetCartProducts()
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");

            var response = await _http.PostAsJsonAsync("api/cart/products", cart);

            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartResponse>>>();

            return cartProducts.Data;
        }
    }
}
