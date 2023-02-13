namespace Calceus.Server.Services.ProfileService
{
    public interface IProfileService
    {
        Task<ServiceResponse<Profile>> GetProfile();
        Task<ServiceResponse<Profile>> AddOrUpdateProfile(Profile profile);
    }
}
