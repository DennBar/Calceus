namespace Calceus.Server.Services.ColorService
{
    public interface IColorService
    {
        Task<ServiceResponse<ColorResponse>> GetBusinessColors(int page);
        Task<ServiceResponse<List<Color>>> GetColors();
        Task<ServiceResponse<Color>> AddColor(Color color);
        Task<ServiceResponse<Color>> UpdateColor(Color color);
    }
}
