namespace Calceus.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
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

        public async Task<ServiceResponse<List<CartResponse>>> GetCartProducts(List<CartItem> cart)
        {
            var response = new ServiceResponse<List<CartResponse>>
            {
                Data = new List<CartResponse>()
            };

            foreach (var cartItem in cart)
            {
                var product = await _context.Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var store = await _context.Stores
                    .Where(s => s.ProductId == cartItem.ProductId &&
                    s.SizeId == cartItem.SizeId &&
                    s.ColorId == cartItem.ColorId)
                    .Include(s => s.Size)
                    .Include(s => s.Color)
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

        public async Task<ServiceResponse<List<CartResponse>>> GetCartProductsByDb(int? userId = null)
        {
            if (userId == null)
            {
                userId = _authService.GetUserId();
            }

            return await GetCartProducts(await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync());
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int sizeId, int colorId)
        {
            var response = await _context.Carts.
                FirstOrDefaultAsync(c => c.ProductId == productId &&
                c.SizeId == sizeId &&
                c.ColorId == colorId &&
                c.UserId == _authService.GetUserId());

            if (response == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "No existen productos en el carrito"
                };
            }

            _context.Carts.Remove(response);

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<CartResponse>>> StoreCartItems(List<CartItem> cart)
        {
            cart.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());

            _context.Carts.AddRange(cart);

            await _context.SaveChangesAsync();

            return await GetCartProductsByDb();
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            var response = await _context.Carts
                 .FirstOrDefaultAsync(c => c.ProductId == cartItem.ProductId &&
                 c.SizeId == cartItem.SizeId &&
                 c.ColorId == cartItem.ColorId &&
                 c.UserId == _authService.GetUserId());

            if (response == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "No existe este producto en el carrito"
                };
            }

            response.Quantity = cartItem.Quantity;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
