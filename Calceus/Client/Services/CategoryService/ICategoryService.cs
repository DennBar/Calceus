namespace Calceus.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<Category> AdminCategories { get; set; }
        Task GetAdminCategories();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
        Category CreateNewCategory();
    }
}
