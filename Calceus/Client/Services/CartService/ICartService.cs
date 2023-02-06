namespace Calceus.Client.Services.CartService
{
    public interface ICartService
    {
        event Action CartChanged;
        Task AddToCart(CartItem cart);
        Task<List<CartItem>> GetCartItems();
        Task<List<CartResponse>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int sizeId, int colorId);
    }
}
