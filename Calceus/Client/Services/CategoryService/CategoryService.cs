﻿namespace Calceus.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;
        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<Category> AdminCategories { get; set; } = new List<Category>();
        public List<Category> BusinessCategories { get; set; } = new List<Category>();
        public List<Category> CustomerCategories { get; set; } = new List<Category>();
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action CategoryChanged;

        public async Task<Category> AddCategory(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/category/admin", category);

            var newCategory = (await response.Content.ReadFromJsonAsync<ServiceResponse<Category>>()).Data;

            return newCategory;
        }

        public async Task GetAdminCategories(int page)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<CategoryResponse>>($"api/category/admin/{page}");

            if (response != null && response.Data != null)
            {
                AdminCategories = response.Data.Categories;
                PageIndex = response.Data.PageIndex;
                PageCount = response.Data.Pages;
            }

            CategoryChanged?.Invoke();
        }

        public async Task GetBusinessCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/business");

            if (response != null && response.Data != null)
            {
                BusinessCategories = response.Data;
            }
        }

        public async Task<ServiceResponse<Category>> GetCategoryById(int categoryId)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Category>>($"api/category/{categoryId}");

            return response;
        }

        public async Task GetCustomerCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/customer");

            if (response != null && response.Data != null)
            {
                CustomerCategories = response.Data;
            }
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var response = await _http.PutAsJsonAsync("api/category/admin", category);

            var updatedCategory = (await response.Content.ReadFromJsonAsync<ServiceResponse<Category>>()).Data;

            return updatedCategory;
        }
    }
}
