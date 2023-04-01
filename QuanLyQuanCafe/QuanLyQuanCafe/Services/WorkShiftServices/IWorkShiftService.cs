using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;

namespace QuanLyQuanCafe.Services.WorkShiftServices
{
    public interface IWorkShiftService
    {
     
        Task<ApiResponse<List<WorkShift>>> GetWorkShift();
        Task<ApiResponse<WorkShift>> CreateWorkShift(WorkShiftDto WorkShiftDto);

        Task<ApiResponse<WorkShift>> UpdateWorkShift(int Id, WorkShiftDto WorkShiftDto);
        Task<ApiResponse<AnyType>> DeleteWorkShift(int Id);
    }
}
