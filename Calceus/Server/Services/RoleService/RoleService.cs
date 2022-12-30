namespace Calceus.Server.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Role>>> GetRoles()
        {
            var roles = await _context.Roles
                .Where(r => r.Name != "admin")
                .ToListAsync();

            return new ServiceResponse<List<Role>>
            {
                Data = roles
            };
        }
    }
}
