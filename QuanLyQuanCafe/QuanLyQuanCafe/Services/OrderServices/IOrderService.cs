using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.OrderServices
{
    public interface IOrderService
    {
        Task<ApiResponse<Order>> GetOrderById(string Id);
        Task<ApiResponse<List<OrderGet>>> GetAllOrder(int page,string? typeSearch, string? searchValue);
        Task<ApiResponse<Order>> CreateOrder(OrderDto orderDto);
        Task<ApiResponse<Order>> UpdateOrder(string Id, OrderDto orderDto);
        Task<ApiResponse<AnyType>> DeleteOrder(string Id);
        Task<ApiResponse<List<OrderGet>>> GetOrderPaid(int page, string? typeSearch, string? searchValue);
        Task<ApiResponse<List<OrderGet>>> GetOrderUnpaid(int page, string? typeSearch, string? searchValue,string? timeStart,string? timeEnd);
        Task<ApiResponse<AnyType>> GraftOrder(string IdOldOrder,string IdNewOrder);
        Task<ApiResponse<AnyType>> SplitOrder(string IdOldOrder, string IdNewOrder, List<DataSplitOrder>? SplitOrders);
    }
}
