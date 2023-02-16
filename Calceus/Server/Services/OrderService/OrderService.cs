namespace Calceus.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        public OrderService(DataContext context, ICartService cartService, IAuthService authService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
        }

        public async Task<ServiceResponse<OrderResponse>> GetCustomerOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderResponse>();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product.Images)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Color)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Size)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                response.Success = false;
                response.Message = "Orden no encontrado";
                return response;
            }

            var orderResponse = new OrderResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsResponse>()
            };

            order.OrderItems.ForEach(o =>
            orderResponse.Products.Add(new OrderDetailsResponse
            {
                ProductId = (int)o.ProductId,
                ImageUrl = o.Product.Images[0].Data,
                Size = o.Size.Ec,
                Color = o.Color.Name,
                Quantity = o.Quantity,
                Title = o.Product.Name,
                TotalPrice = o.TotalPrice,
            }));

            response.Data = orderResponse;

            return response;
        }

        public async Task<ServiceResponse<List<OrderCustomerResponse>>> GetCustomerOrders()
        {
            var response = new ServiceResponse<List<OrderCustomerResponse>>();

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)                
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderCustomerResponse = new List<OrderCustomerResponse>();

            orders.ForEach(o => orderCustomerResponse.Add(new OrderCustomerResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice                                
            }));

            response.Data = orderCustomerResponse;

            return response;
        }

        public async Task<ServiceResponse<List<OrderVendorResponse>>> GetVendorOrders()
        {
            var response = new ServiceResponse<List<OrderVendorResponse>>();

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Color)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Size)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Images)
                .Select(o => o.OrderItems.Where(oi => oi.Product.UserId == _authService.GetUserId()))
                .ToListAsync();

            var orderVendorResponse = new List<OrderVendorResponse>();

            orders.ForEach(o => orderVendorResponse.Add(new OrderVendorResponse
            {
                OrderVendorItems = o.ToList(),
            }));

            response.Data = orderVendorResponse;

            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            var products = (await _cartService.GetCartProductsByDb(_authService.GetUserId())).Data;

            decimal totalPrice = 0;

            products.ForEach(p => totalPrice += p.Price * p.Quantity);

            var orderItems = new List<OrderItem>();

            products.ForEach(p => orderItems.Add(new OrderItem
            {
                ProductId = p.ProductId,
                SizeId = p.SizeId,
                ColorId = p.ColorId,
                Quantity = p.Quantity,
                TotalPrice = p.Price * p.Quantity
            }));

            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems,
            };

            _context.Orders.Add(order);

            _context.Carts.RemoveRange(_context.Carts.Where(c => c.UserId == _authService.GetUserId()));

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }


    }
}



