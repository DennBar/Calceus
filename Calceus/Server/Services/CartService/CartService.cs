namespace Calceus.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext dataContext, IAuthService authService)
        {
            _context = dataContext;
            _authService = authService;
        }

        public async Task<ServiceResponse<bool>> AddToCart(Cart cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var currentCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.ProductId == cartItem.ProductId &&
                c.SizeId == cartItem.SizeId &&
                c.ColorId == cartItem.ColorId &&
                c.UserId == cartItem.UserId);

            if (currentCartItem == null)
            {
                _context.Carts.Add(cartItem);
            }
            else
            {
                currentCartItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.Carts.Where(c => c.UserId == _authService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartResponse>>> GetCartProducts(List<Cart> cart)
        {
            var response = new ServiceResponse<List<CartResponse>>
            {
                Data = new List<CartResponse>()
            };

            foreach (var cartItem in cart)
            {
                var product = await _context.Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var store = await _context.Stores
                    .Where(s => s.ProductId == cartItem.ColorId &&
                    s.SizeId == cartItem.SizeId &&
                    s.ColorId == cartItem.ColorId)
                    .FirstOrDefaultAsync();

                if (store == null)
                {
                    continue;
                }

                var cartProduct = new CartResponse
                {
                    ProductId = product.Id,
                    Title = product.Name,
                    ImageUrl = product.Images[0].Data,
                    Price = store.Price,
                    SizeId = store.SizeId,
                    Size = store.Size.Ec,
                    ColorId = store.ColorId,
                    Color = store.Color.Name,
                    Quantity = store.Quantity,
                };

                response.Data.Add(cartProduct);
            }

            return response;
        }

        public Task<ServiceResponse<List<CartResponse>>> GetCartProductsByDb(int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int sizeId, int colorId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<CartResponse>>> StoreCartItems(List<Cart> cart)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> UpdateQuantity(Cart cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
