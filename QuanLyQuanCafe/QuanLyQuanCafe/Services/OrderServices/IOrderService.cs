using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.OrderServices
{
    public interface IOrderService
    {
        Task<ApiResponse<Order>> GetOrderById(string Id);
        Task<ApiResponse<List<Order>>> GetAllOrder();
        Task<ApiResponse<Order>> CreateOrder(OrderDto orderDto);
        Task<ApiResponse<Order>> UpdateOrder(string Id, OrderDto orderDto);
        Task<ApiResponse<AnyType>> DeleteOrder(string Id);
        Task<ApiResponse<List<Order>>> GetOrderPaid();
        Task<ApiResponse<List<Order>>> GetOrderUnpaid();
    }
}
