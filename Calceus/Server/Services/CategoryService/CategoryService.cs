namespace Calceus.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Category>> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Category> { Data = category };
        }

        public async Task<ServiceResponse<Category>> UpdateCategory(Category category)
        {
            var response = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (response == null)
            {
                return new ServiceResponse<Category>
                {
                    Success = false,
                    Message = "Categoría no encontrada"
                };
            }

            response.Name = category.Name;
            response.Url = category.Url;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Category>
            {
                Data = category
            };

        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
        {
            var categories = await _context.Categories.Where(c => !c.Deleted).ToListAsync();

            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }

        private async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<ServiceResponse<List<Category>>> GetBusinessCategories()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Category>>> GetCustomerCatagories()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<CategoryResponse>> GetAdminCategories(int page)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<Category>> ICategoryService.GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }
    }

}
