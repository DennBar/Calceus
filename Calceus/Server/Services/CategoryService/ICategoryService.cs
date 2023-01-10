namespace Calceus.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetBusinessCategories();
        Task<ServiceResponse<List<Category>>> GetCustomerCatagories();
        Task<ServiceResponse<CategoryResponse>> GetAdminCategories(int page);
        Task<ServiceResponse<Category>> GetCategoryById(int categoryId);
        Task<ServiceResponse<Category>> AddCategory(Category category);
        Task<ServiceResponse<Category>> UpdateCategory(Category category);
    }
}
