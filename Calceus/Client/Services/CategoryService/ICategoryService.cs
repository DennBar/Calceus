namespace Calceus.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action CategoryChanged;
        int PageIndex { get; set; }
        int PageCount { get; set; }
        List<Category> AdminCategories { get; set; }
        List<Category> BusinessCategories { get; set; }
        List<Category> CustomerCategories { get; set; }
        Task GetAdminCategories(int page);
        Task GetBusinessCategories();
        Task GetCustomerCategories();
        Task<ServiceResponse<Category>> GetCategoryById(int categoryId);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
    }
}
