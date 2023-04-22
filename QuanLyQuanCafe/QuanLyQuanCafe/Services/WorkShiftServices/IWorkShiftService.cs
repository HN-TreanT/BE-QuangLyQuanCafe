using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.WorkShift;

namespace QuanLyQuanCafe.Services.WorkShiftServices
{
    public interface IWorkShiftService
    {

        Task<ApiResponse<WorkShift>> GetWorkShiftDetail(int Id);
        Task<ApiResponse<List<WorkShift>>> GetWorkShift();
        Task<ApiResponse<WorkShift>> CreateWorkShift(WorkShiftDto WorkShiftDto);

        Task<ApiResponse<WorkShift>> UpdateWorkShift(int Id, UpdateWorkShiftDto WorkShiftDto);
        Task<ApiResponse<AnyType>> DeleteWorkShift(int Id);
    }
}
