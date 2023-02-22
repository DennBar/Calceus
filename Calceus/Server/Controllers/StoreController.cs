using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("product/{productId}"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<List<StoreResponse>>>> GetStoreByProductIdGroupBySize(int productId)
        {
            var response = await _storeService.GetStoreByProductIdGroupBySize(productId);

            return Ok(response);
        }

    }
}
