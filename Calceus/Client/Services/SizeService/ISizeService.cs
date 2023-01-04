namespace Calceus.Client.Services.SizeService
{
    public interface ISizeService
    {
        event Action SizeChanged;
        List<Size> Sizes { get; set; }
        int PageIndex { get; set; }
        int PageCount { get; set; }
        Task GetSizes(int page);
    }
}
