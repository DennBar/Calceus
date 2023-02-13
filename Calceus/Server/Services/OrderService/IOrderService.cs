namespace Calceus.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderVendorResponse>>> GetVendorOrders();
        Task<ServiceResponse<List<OrderCustomerResponse>>> GetCustomerOrders();
        Task<ServiceResponse<OrderResponse>> GetCustomerOrderDetails(int orderId);
    }
}
