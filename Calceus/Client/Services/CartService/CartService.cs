namespace Calceus.Client.Services.CartService
{
    public class CartService : ICartService
    {
        public event Action CartChanged;

        public Task AddToCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cart>> GetCart()
        {
            throw new NotImplementedException();
        }
    }
}
