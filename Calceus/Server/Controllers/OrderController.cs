using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calceus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
        {
            var response = await _orderService.PlaceOrder();

            return Ok(response);
        }

        [HttpGet("business"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<List<OrderVendorResponse>>>> GetVendorOrders()
        {
            var response = await _orderService.GetVendorOrders();

            return Ok(response);
        }

        [HttpGet("business/{productId}"), Authorize(Roles = "business")]
        public async Task<ActionResult<ServiceResponse<List<OrderSellerResponse>>>> GetSellerOrders(int productId)
        {
            var response = await _orderService.GetSellerOrders(productId);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderCustomerResponse>>>> GetCustomerOrders()
        {
            var response = await _orderService.GetCustomerOrders();

            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderResponse>>> GetCustomerOrderDetails(int orderId)
        {
            var response = await _orderService.GetCustomerOrderDetails(orderId);

            return Ok(response);
        }
    }
}
