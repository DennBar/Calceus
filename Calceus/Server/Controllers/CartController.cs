using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartResponse>>>> GetCartProducts(List<CartItem> cartItems)
        {
            var response = await _cartService.GetCartProducts(cartItems);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartResponse>>>> StoreCartItems(List<CartItem> cartItems)
        {
            var response = await _cartService.StoreCartItems(cartItems);

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
        {
            var response = await _cartService.AddToCart(cartItem);

            return Ok(response);
        }

        [HttpPut("quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartItem cartItem)
        {
            var response = await _cartService.UpdateQuantity(cartItem);

            return Ok(response);
        }

        [HttpDelete("{productId/sizeId/colorId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCart(int productId, int sizeId, int colorId)
        {
            var response = await _cartService.RemoveItemFromCart(productId, sizeId, colorId);

            return Ok(response);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCounts()
        {
            var response = await _cartService.GetCartItemsCount();

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartResponse>>>> GetCartProductsByDb()
        {
            var response = await _cartService.GetCartProductsByDb();

            return Ok(response);
        }

    }
}
