namespace Calceus.Server.Services.ColorService
{
    public interface IColorService
    {
        Task<ServiceResponse<ColorResponse>> GetAllMyColors(int page);
        Task<ServiceResponse<List<Color>>> GetMyColors();
        Task<ServiceResponse<Color>> GetColorById(int colorId);
        Task<ServiceResponse<Color>> AddMyColor(Color color);
        Task<ServiceResponse<Color>> UpdateMyColor(Color color);
    }
}
