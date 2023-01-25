using Calceus.Server.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("business/me/{page}"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<ProductResponse>>> GetMyProducts(int page = 1)
        {
            var response = await _productService.GetMyProducts(page);

            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetMyProductById(int productId)
        {
            var response = await _productService.GetMyProductById(productId);

            return Ok(response);
        }

        [HttpPost("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<Product>>> AddProduct(Product product)
        {
            var response = await _productService.AddProduct(product);

            return Ok(response);
        }

        [HttpPut("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Product product)
        {
            var response = await _productService.UpdateProduct(product);

            return Ok(response);
        }

        [HttpPut("business/store"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpsertMyStoreByProduct(Product product)
        {
            var response = await _productService.UpsertMyStoreByProduct(product);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();

            return Ok(response);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
        {
            var response = await _productService.GetProductsByCategory(categoryUrl);

            return Ok(response);
        }
    }
}
