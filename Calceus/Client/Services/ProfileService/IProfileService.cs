namespace Calceus.Client.Services.ProfileService
{
    public interface IProfileService
    {
        Task<Profile> GetProfile();
        Task<Profile> AddOrUpdateProfile(Profile profile);
    }
}
