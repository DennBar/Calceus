namespace Calceus.Server.Services.SizeService
{
    public interface ISizeService
    {
        Task<ServiceResponse<SizeResponse>> GetAdminSizes(int page);
        Task<ServiceResponse<List<Size>>> GetBusinessSizesByCategory(int categoryId);        
        Task<ServiceResponse<Size>> GetSizeById(int sizeId);
        Task<ServiceResponse<Size>> AddSize(Size size);
        Task<ServiceResponse<Size>> UpdateSize(Size size);
    }
}
