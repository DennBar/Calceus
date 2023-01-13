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

            return new ServiceResponse<Category>
            {
                Data = category
            };
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

        public async Task<ServiceResponse<CategoryResponse>> GetAdminCategories(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling((await _context.Categories.ToListAsync()).Count / pageResults);
            var categories = await _context.Categories
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new ServiceResponse<CategoryResponse>
            {
                Data = new CategoryResponse
                {
                    Categories = categories,
                    PageIndex = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetBusinessCategories()
        {
            var response = await _context.Categories.ToListAsync();

            return new ServiceResponse<List<Category>>
            {
                Data = response
            };

        }

        public async Task<ServiceResponse<List<Category>>> GetCustomerCatagories()
        {
            var response = await _context.Categories.ToListAsync();

            return new ServiceResponse<List<Category>>
            {
                Data = response
            };
        }

        public async Task<ServiceResponse<Category>> GetCategoryById(int categoryId)
        {
            var response = new ServiceResponse<Category>();

            Category category = null;

            category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category != null)
            {
                response.Data = category;
            }
            else
            {
                response.Success = false;
                response.Message = "Esta categoría no existe";
            }

            return response;
        }

    }

}
