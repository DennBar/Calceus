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
            var result = await _categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpPut("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> UpdateCategory(Category category)
        {
            var result = await _categoryService.UpdateCategory(category);
            return Ok(result);
        }

        [HttpDelete("admin/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }

        [HttpGet("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAdminCategories()
        {
            var result = await _categoryService.GetAdminCategories();
            return Ok(result);
        }
    }
}
