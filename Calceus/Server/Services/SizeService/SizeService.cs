namespace Calceus.Server.Services.SizeService
{
    public class SizeService : ISizeService
    {
        private readonly DataContext _context;

        public SizeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Size>> AddSize(Size size)
        {
            _context.Sizes.Add(size);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Size> { Data = size };
        }

        public async Task<ServiceResponse<Size>> UpdateSize(Size size)
        {
            var response = await _context.Sizes
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == size.Id);

            if (response == null)
            {
                return new ServiceResponse<Size>
                {
                    Success = false,
                    Message = "Talla no encontrada"

                };
            }

            response.Cm = size.Cm;
            response.Ec = size.Ec;
            response.Usa = size.Usa;
            response.Eu = size.Eu;
            response.CategoryId = size.CategoryId;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Size>
            {
                Data = size
            };
        }

        public async Task<ServiceResponse<SizeResponse>> GetAdminSizes(int page)
        {
            var pageSize = 10f;
            var pageCount = Math.Ceiling((await _context.Sizes.ToListAsync()).Count / pageSize);
            var sizes = await _context.Sizes
                .Include(s => s.Category)
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToListAsync();

            var response = new ServiceResponse<SizeResponse>
            {
                Data = new SizeResponse
                {
                    Sizes = sizes,
                    PageIndex = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<Size>>> GetBusinessSizesByCategory(int categoryId)
        {
            var response = await _context.Sizes
                .Include(s => s.Category)
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();

            return new ServiceResponse<List<Size>>
            {
                Data = response
            };
        }

        public async Task<ServiceResponse<Size>> GetSizeById(int sizeId)
        {
            var response = new ServiceResponse<Size>();

            Size size = null;

            size = await _context.Sizes
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == sizeId);

            if (size != null)
            {
                response.Data = size;
            }
            else
            {
                response.Success = false;
                response.Message = "Esta talla no existe";
            }

            return response;
        }
    }
}
