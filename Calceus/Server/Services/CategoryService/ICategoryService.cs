namespace Calceus.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetBusinessCategories();
        Task<ServiceResponse<List<Category>>> GetCustomerCatagories();
        Task<ServiceResponse<List<Category>>> GetAdminCategories();
        Task<ServiceResponse<Category>> GetCategoryById(int categoryId);
        Task<ServiceResponse<List<Category>>> AddCategory(Category category);
        Task<ServiceResponse<List<Category>>> UpdateCategory(Category category);
    }
}
