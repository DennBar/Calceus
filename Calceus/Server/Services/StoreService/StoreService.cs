namespace Calceus.Server.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly DataContext _context;

        public StoreService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<StoreResponse>>> GetStoreByProductIdGroupBySize(int productId)
        {
            var response = _context.Stores
                .Where(p => p.ProductId == productId)
                .Include(p => p.Color)
                .GroupBy(g => g.SizeId)
                .ToList()
                .Select(s => new StoreResponse
                {
                    SizeId = s.Key,
                    Stores = s.Select(y => y)
                })
                .OrderBy(e => e.SizeId)
                .ToList();

            return new ServiceResponse<List<StoreResponse>>
            {
                Data = response
            };
        }
    }
}
