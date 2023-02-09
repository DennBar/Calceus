namespace Calceus.Client.Services.CartService
{
    public interface ICartService
    {
        event Action CartChanged;
        Task AddToCart(CartItem cartItem);        
        Task<List<CartResponse>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int sizeId, int colorId);
        Task UpdateQuantity(CartResponse product);
        Task StoreCartItems(bool emptyLocalCart);
        Task GetCartItemsCount();
    }
}
