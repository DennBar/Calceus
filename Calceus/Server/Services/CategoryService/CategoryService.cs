﻿namespace Calceus.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
        {
            category.Editing = category.IsNew = false;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
        {
            var cat = await GetCategoryById(category.Id);

            if (cat == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Categoría no encontrada"
                };
            }

            cat.Name = category.Name;
            cat.Url = category.Url;            

            await _context.SaveChangesAsync();

            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategory(int id)
        {
            Category category = await GetCategoryById(id);

            if (category == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Categoría no encontrada"
                };
            }

            category.Deleted = true;
            await _context.SaveChangesAsync();

            return await GetAdminCategories();
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
    }

}
