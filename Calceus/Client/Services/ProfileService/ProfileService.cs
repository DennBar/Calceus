namespace Calceus.Client.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _http;

        public ProfileService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Profile> AddOrUpdateProfile(Profile profile)
        {
            var response = await _http.PostAsJsonAsync("api/profile", profile);

            return response.Content.ReadFromJsonAsync<ServiceResponse<Profile>>().Result.Data;
        }

        public async Task<Profile> GetProfile()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Profile>>("api/profile");

            return response.Data;
        }
    }
}
