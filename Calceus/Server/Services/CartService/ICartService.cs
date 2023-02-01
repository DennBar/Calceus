namespace Calceus.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartResponse>>> GetCartProducts(List<Cart> cart);
        Task<ServiceResponse<List<CartResponse>>> StoreCartItems(List<Cart> cart);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartResponse>>> GetCartProductsByDb(int? userId = null);
        Task<ServiceResponse<bool>> AddToCart(Cart cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(Cart cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int sizeId, int colorId);
    }
}
