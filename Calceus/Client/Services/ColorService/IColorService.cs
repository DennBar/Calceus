﻿namespace Calceus.Client.Services.ColorService
{
    public interface IColorService
    {
        event Action ColorChanged;
        int PageIndex { get; set; }
        int PageCount { get; set; }
        List<Color> AllMyColors { get; set; }
        List<Color> MyColors { get; set; }
        Task GetAllMyColors(int page);
        Task GetMyColors();
        Task<ServiceResponse<Color>> GetColorById(int colorId);
        Task<Color> AddMyColor(Color color);
        Task<Color> UpdateMyColor(Color color);
    }
}
