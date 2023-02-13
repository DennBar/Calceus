using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Profile>>> GetProfile()
        {
            var response = await _profileService.GetProfile();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Profile>>> AddOrUpdateProfile(Profile profile)
        {
            var response = await _profileService.AddOrUpdateProfile(profile);

            return response;
        }
    }
}
