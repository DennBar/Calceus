namespace Calceus.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartResponse>>> GetCartProducts(List<CartItem> cart);
        Task<ServiceResponse<List<CartResponse>>> StoreCartItems(List<CartItem> cart);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartResponse>>> GetCartProductsByDb(int? userId = null);
        Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int sizeId, int colorId);
    }
}
