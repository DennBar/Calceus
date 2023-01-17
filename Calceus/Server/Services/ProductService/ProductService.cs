namespace Calceus.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public ProductService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Product>> AddProduct(Product product)
        {
            product.UserId = _authService.GetUserId();

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product,
            };
        }

        public async Task<ServiceResponse<Product>> GetMyProductById(int productId)
        {
            var response = new ServiceResponse<Product>();

            Product product = null;

            product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId && p.UserId == _authService.GetUserId());

            if (product != null)
            {
                response.Data = product;
            }
            else
            {
                response.Success = false;
                response.Message = "Este producto no existe";
            }

            return response;
        }

        public async Task<ServiceResponse<ProductResponse>> GetMyProducts(int page)
        {
            var pageSize = 10f;
            var pageCount = Math.Ceiling((await _context.Products.ToListAsync()).Count / pageSize);
            var products = await _context.Products
                .Where(p => p.UserId == _authService.GetUserId())
                .Include(p => p.Category)
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            var response = new ServiceResponse<ProductResponse>
            {
                Data = new ProductResponse
                {
                    Products = products,
                    PageIndex = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var response = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == product.Id && p.UserId == _authService.GetUserId());

            if (product == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Producto no encontrado"
                };
            }

            response.Name = product.Name;
            response.Description = product.Description;
            response.CategoryId = product.CategoryId;
            response.Visible = product.Visible;

            var productImages = response.Images;
            _context.Images.RemoveRange(productImages);

            response.Images = product.Images;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = response
            };
        }
    }
}
