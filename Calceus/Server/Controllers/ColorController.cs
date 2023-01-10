using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("business/all/{page}"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<ColorResponse>>> GetMyBusinessColors(int page = 1)
        {
            var response = _colorService.GetMyBusinessColors(page);
            return Ok(response);
        }

        [HttpGet, Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<List<Color>>>> GetMyColors()
        {
            var response = _colorService.GetMyColors();
            return Ok(response);
        }

        [HttpPost("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<Color>>> AddMyColor(Color color)
        {
            var response = await _colorService.AddMyColor(color);
            return Ok(response);
        }

        [HttpPut("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<Color>>> UpdateMyColor(Color color)
        {
            var response = await _colorService.UpdateMyColor(color);
            return Ok(response);
        }
    }
}
