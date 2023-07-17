namespace Calceus.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> PlaceOrder();
        Task<List<OrderCustomerResponse>> GetCustomerOrders();
        Task<List<OrderVendorResponse>> GetVendorOrders();
        Task<List<OrderSellerResponse>> GetSellerOrders(int productId);
        Task<OrderResponse> GetCustomerOrderDetails(int orderId);
    }
}
