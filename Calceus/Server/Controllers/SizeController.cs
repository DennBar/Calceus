using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("admin/all/{page}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<SizeResponse>>> GetAdminSizes(int page = 1)
        {
            var response = await _sizeService.GetAdminSizes(page);

            return Ok(response);
        }

        [HttpPost("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<Size>>> AddSize(Size size)
        {
            var response = await _sizeService.AddSize(size);

            return Ok(response);
        }

        [HttpPut("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<Size>>> UpdateSize(Size size)
        {
            var response = await _sizeService.UpdateSize(size);

            return Ok(response);
        }

        [HttpGet("{sizeId}")]
        public async Task<ActionResult<ServiceResponse<Size>>> GetSizeById(int sizeId)
        {
            var response = await _sizeService.GetSizeById(sizeId);

            return Ok(response);
        }

        [HttpGet("business/{categoryId}")]
        public async Task<ActionResult<ServiceResponse<List<Size>>>> GetSizesByCategoryId(int categoryId)
        {
            var response = await _sizeService.GetSizesByCategoryId(categoryId);
            return Ok(response);
        }
    }
}
