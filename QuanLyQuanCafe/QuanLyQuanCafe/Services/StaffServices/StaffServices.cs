using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace QuanLyQuanCafe.Services.StaffServices
{
    public class StaffServices:IStaffService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public StaffServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<List<staff>>> GetAllStaff()
        {
            var response = new ApiResponse<List<staff>>();
            try
            {
                var dbStaffs = await _context.staff.ToListAsync();
                if(dbStaffs.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbStaffs;   
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<staff>> getStaff(string Id)
        {
            var response = new ApiResponse<staff>();
            try
            {
                var dbStaff = await _context.staff.FindAsync(Id);
                if(dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "not found ";
                    return response;
                }
                response.Data = dbStaff;    

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<staff>> CreateStaff(StaffDto StaffDto)
        {
            var response = new ApiResponse<staff>();
            try
            {

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<staff>> UpdateInfoStaff(string Id, StaffDto staffDto)
        {
            var response = new ApiResponse<staff>();
            try
            {

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteStaff(string Id)
        {
            var response = new ApiResponse<AnyType>();
            try
            {

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
