using Blazored.LocalStorage;
using Calceus.Shared;

namespace Calceus.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
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
        }

        public async Task<List<Cart>> GetCart()
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");

            if (cart == null)
            {
                cart = new List<Cart>();
            }

            return cart;
        }
    }
}
