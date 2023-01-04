namespace Calceus.Server.Services.SizeService
{
    public class SizeService : ISizeService
    {
        private readonly DataContext _context;

        public SizeService(DataContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<Size>> AddSize(Size size)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Size>> GetSizeById(int sizeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<SizeResponse>> GetSizes(int page)
        {
            var pageSize = 2f;
            var pageCount = Math.Ceiling((await _context.Sizes.ToListAsync()).Count / pageSize);
            var sizes = await _context.Sizes
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



        public Task<ServiceResponse<Size>> UpdateSize(Size size)
        {
            throw new NotImplementedException();
        }
    }
}
