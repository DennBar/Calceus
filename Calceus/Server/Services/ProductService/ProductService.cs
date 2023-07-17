using System.Linq;

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

        public async Task<ServiceResponse<Product>> GetProductById(int productId)
        {
            var response = new ServiceResponse<Product>();

            Product product = null;

            product = await _context.Products
                .Where(p => p.Id == productId)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p=>p.Stores)
                .FirstOrDefaultAsync();

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
                .Include(p=>p.Images)
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

        public async Task<ServiceResponse<Product>> UpsertMyStoreByProduct(Product product)
        {
            var response = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id && p.UserId == _authService.GetUserId());

            if (response == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Producto no encontrado"
                };
            }

            foreach (var storeItem in product.Stores)
            {
                var store = await _context.Stores
                    .SingleOrDefaultAsync(s =>
                    s.ProductId == storeItem.ProductId &&
                    s.ColorId == storeItem.ColorId &&
                    s.SizeId == storeItem.SizeId);

                if (store == null)
                {
                    storeItem.Color = null;
                    storeItem.Size = null;                  
                    storeItem.UserId = _authService.GetUserId();
                    _context.Stores.Add(storeItem);
                }
                else
                {
                    store.ColorId = storeItem.ColorId;
                    store.SizeId = storeItem.SizeId;
                    store.Visible = storeItem.Visible;
                    store.Price = storeItem.Price;
                    store.Quantity = storeItem.Quantity;
                }
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product
            };
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

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var products = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .Include(p => p.Category)
                .Include(p => p.Stores)
                .Include(p => p.Images)
                .ToListAsync();

            var response = new ServiceResponse<List<Product>>
            {
                Data = products
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProducts()
        {
            var products = await _context.Products
               .Include(p => p.Category)
               .Include(p => p.Stores)
               .Where(p => p.Stores.Any(s => s.Quantity > 0))
               .Include(p => p.Images)
               .ToListAsync();

            var response = new ServiceResponse<List<Product>>
            {
                Data = products
            };

            return response;
        }
    }
}
