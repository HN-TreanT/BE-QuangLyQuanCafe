using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;
namespace QuanLyQuanCafe.Services.StaffServices
{
    public interface IStaffService
    {
        Task<ApiResponse<staff>> getStaff(string Id);
        Task<ApiResponse<List<staff>>> GetAllStaff();
        Task<ApiResponse<staff>> CreateStaff(StaffDto StaffDto);

        Task<ApiResponse<staff>> UpdateInfoStaff(string Id, StaffDto staffDto);
        Task<ApiResponse<AnyType>> DeleteStaff(string Id);
    }
}
