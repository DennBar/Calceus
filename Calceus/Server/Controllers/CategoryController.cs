using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategory(Category category)
        {
            var response = await _categoryService.AddCategory(category);
            return Ok(response);
        }

        [HttpPut("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> UpdateCategory(Category category)
        {
            var response = await _categoryService.UpdateCategory(category);
            return Ok(response);
        }

        [HttpGet("admin/{page}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<CategoryResponse>>> GetAdminCategories(int page = 1)
        {
            var response = await _categoryService.GetAdminCategories(page);
            return Ok(response);
        }

        [HttpGet("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetBusinessCategories()
        {
            var response = await _categoryService.GetBusinessCategories();
            return Ok(response);
        }

        [HttpGet("customer")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCustomerCategories()
        {
            var response = await _categoryService.GetCustomerCatagories();
            return Ok(response);
        }
    }
}
