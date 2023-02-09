using Blazored.LocalStorage;

namespace Calceus.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        public CartService(ILocalStorageService localStorage, HttpClient http, IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
        }

        public event Action CartChanged;

        public async Task AddToCart(CartItem cartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add", cartItem);
            }
            else
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
            }

            await GetCartItemsCount();
        }       

        public async Task GetCartItemsCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");

                var count = response.Data;

                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
            }

            CartChanged.Invoke();
        }

        public async Task<List<CartResponse>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartResponse>>>("api/cart");

                return response.Data;
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                if (cart == null)
                {
                    return new List<CartResponse>();
                }

                var response = await _http.PostAsJsonAsync("api/cart/products", cart);

                var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartResponse>>>();

                return cartProducts.Data;
            }
        }

        public async Task RemoveProductFromCart(int productId, int sizeId, int colorId)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"api/cart/{productId}/{sizeId}/{colorId}");
            }

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
            }
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (localCart == null)
            {
                return;
            }

            await _http.PostAsJsonAsync("api/cart", localCart);

            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task UpdateQuantity(CartResponse product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    SizeId = product.SizeId,
                    ColorId = product.ColorId,
                };

                await _http.PutAsJsonAsync("api/cart/quantity", response);
            }
            else
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
}
