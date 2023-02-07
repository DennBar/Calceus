using Blazored.LocalStorage;

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

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var currentItem = cart.Find(ci => ci.ProductId == cartItem.ProductId && ci.SizeId == cartItem.SizeId && ci.ColorId == cartItem.ColorId);

            if (currentItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                currentItem.Quantity += cartItem.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);
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

        public async Task RemoveProductFromCart(int productId, int sizeId, int colorId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == productId && x.SizeId == sizeId && x.ColorId == colorId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                CartChanged.Invoke();
            }
        }

        public async Task UpdateQuantity(CartResponse product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.SizeId == product.SizeId && x.ColorId == product.ColorId);

            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}
