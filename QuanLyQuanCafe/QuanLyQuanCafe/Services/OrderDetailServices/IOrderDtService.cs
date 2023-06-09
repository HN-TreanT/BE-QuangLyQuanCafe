using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Dto.OrderDetail;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.OrderDetailServices
{
    public interface IOrderDtService
    {
        Task<ApiResponse<OrderDetail>> GetOrderDtById(string Id);
        Task<ApiResponse<List<OrderDetail>>> GetAllOrderDt();
        Task<ApiResponse<OrderDetail>> CreateOrderDt(OrderDetailDto orderDetailDto);
        Task<ApiResponse<OrderDetail>> UpdateOrderDt(string Id, UpdateOrderDetail updateOrderDetailDto); 
        Task<ApiResponse<AnyType>>  DeleteOrderDt(string Id);
        Task<ApiResponse<List<OrderDetail>>> GetAllOrderDtByIdOrder(string IdOrder);
        Task<ApiResponse<List<OrderDetail>>> CreateListOrderDt(List<OrderDetailDto> ListOrderDt);
        //Lấy ra tiền hàng , giảm giá , số khách hàng , số hóa đơn,số mặt hàng 
        Task<ApiResponse<Overview>> GetOverview(int time);

        Task<ApiResponse<RevenueOverview>> RevenueOverview();
    }
}
