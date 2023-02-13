namespace Calceus.Server.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public ProfileService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Profile>> AddOrUpdateProfile(Profile profile)
        {
            var response = new ServiceResponse<Profile>();

            var profileFromDB = (await GetProfile()).Data;

            if (profileFromDB == null)
            {
                profile.UserId = _authService.GetUserId();
                _context.Profile.Add(profile);
                response.Data = profile;
            }
            else
            {
                profileFromDB.FirstName = profile.FirstName;
                profileFromDB.LastName = profile.LastName;
                profileFromDB.Street = profile.Street;
                profileFromDB.City = profile.City;
                profileFromDB.PostalCode = profile.PostalCode;
                profileFromDB.Province = profile.Province;
                profileFromDB.Country = profile.Country;

                response.Data = profileFromDB;
            }

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<Profile>> GetProfile()
        {
            int userId = _authService.GetUserId();

            var profile = await _context.Profile.FirstOrDefaultAsync(a => a.UserId == userId);

            return new ServiceResponse<Profile> { Data = profile };
        }
    }
}
