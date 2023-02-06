namespace Calceus.Client.Services.CartService
{
    public interface ICartService
    {
        event Action CartChanged;
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetCartItems();
        Task<List<CartResponse>> GetCartProducts();
    }
}
