namespace Calceus.Server.Services.SizeService
{
    public interface ISizeService
    {
        Task<ServiceResponse<SizeResponse>> GetSizes(int page);
        Task<ServiceResponse<Size>> GetSizeById(int sizeId);
        Task<ServiceResponse<Size>> AddSize(Size size);
        Task<ServiceResponse<Size>> UpdateSize(Size size);
    }
}
