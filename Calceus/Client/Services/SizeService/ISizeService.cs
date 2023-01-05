namespace Calceus.Client.Services.SizeService
{
    public interface ISizeService
    {
        event Action SizeChanged;
        List<Size> Sizes { get; set; }
        int PageIndex { get; set; }
        int PageCount { get; set; }
        Task GetSizes(int page);
        Task<ServiceResponse<Size>> GetSizeById(int sizeId);
        Task<Size> AddSize(Size size);
        Task<Size> UpdateSize(Size size);

    }
}
