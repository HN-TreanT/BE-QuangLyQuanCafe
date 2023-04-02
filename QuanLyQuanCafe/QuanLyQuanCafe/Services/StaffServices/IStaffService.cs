using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Staff;
using Newtonsoft.Json.Linq;

namespace QuanLyQuanCafe.Services.StaffServices
{
    public interface IStaffService
    {
        Task<ApiResponse<staff>> getStaff(string Id);
        Task<ApiResponse<List<staff>>> GetAllStaff();
        Task<ApiResponse<staff>> CreateStaff(StaffCreateDto StaffDto);

        Task<ApiResponse<staff>> UpdateInfoStaff(string Id, StaffCreateDto staffDto);
        Task<ApiResponse<AnyType>> DeleteStaff(string Id);

        Task<ApiResponse<List<staff>>> SearchStaffByName(string staffName);
        Task<ApiResponse<staff>> SearchStaffByPhone(string staffPhone);
    }
}
