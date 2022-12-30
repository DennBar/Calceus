namespace Calceus.Client.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _http;

        public RoleService(HttpClient http)
        {
            _http = http;
        }

        public List<Role> Roles { get; set; } = new List<Role>();

        public async Task GetRoles()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Role>>>("api/role");

            if (response != null && response.Data != null)
            {
                Roles = response.Data;
            }
        }
    }
}
