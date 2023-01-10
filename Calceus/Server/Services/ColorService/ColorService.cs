namespace Calceus.Server.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public ColorService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Color>> AddMyColor(Color color)
        {
            color.UserId = _authService.GetUserId();

            _context.Colors.Add(color);

            await _context.SaveChangesAsync();

            return new ServiceResponse<Color>
            {
                Data = color
            };
        }

        public async Task<ServiceResponse<ColorResponse>> GetMyBusinessColors(int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await _context.Colors
                .Where(c => c.UserId == _authService.GetUserId())
                .ToListAsync()).Count / pageResults);
            var colors = await _context.Colors
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new ServiceResponse<ColorResponse>
            {
                Data = new ColorResponse
                {
                    Colors = colors,
                    PageIndex = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<Color>>> GetMyColors()
        {
            var colors = await _context.Colors.Where(c => c.UserId == _authService.GetUserId()).ToListAsync();

            return new ServiceResponse<List<Color>>
            {
                Data = colors
            };
        }

        public async Task<ServiceResponse<Color>> UpdateMyColor(Color color)
        {
            var response = await _context.Colors
                .FirstOrDefaultAsync(c => c.Id == color.Id && c.UserId == _authService.GetUserId());

            if (response == null)
            {
                return new ServiceResponse<Color>
                {
                    Success = false,
                    Message = "Color no encontrado"
                };
            }

            response.Name = color.Name;
            response.Code = color.Code;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Color>
            {
                Data = color
            };
        }
    }
}
