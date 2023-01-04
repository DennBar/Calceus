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

        [HttpGet("all/{page}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<SizeResponse>>> GetSizes(int page = 1)
        {
            var response = await _sizeService.GetSizes(page);

            return Ok(response);
        }
    }
}
