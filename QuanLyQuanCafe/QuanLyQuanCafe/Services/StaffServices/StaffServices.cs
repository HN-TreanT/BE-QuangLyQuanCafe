using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Staff;

using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

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

                var dbStaff = await _context.staff.Include(s => s.SelectedWorkShifts)
                .SingleOrDefaultAsync(s => s.IdStaff == Id);
                if (dbStaff == null)
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

        public async Task<ApiResponse<staff>> CreateStaff(StaffCreateDto StaffDto)
        {
            var response = new ApiResponse<staff>();
            try
            {
                string IdStaff = Guid.NewGuid().ToString().Substring(0, 10);
                var dbStaff = _context.staff.Where(u => u.IdStaff == IdStaff).FirstOrDefault();
                if (dbStaff != null)
                {
                    response.Status = false;
                    response.Message = "staff already";
                    return response;
                }

                var staff = new staff
                {
                    IdStaff = IdStaff,
                    Fullname = StaffDto.Fullname,
                    Birthday = StaffDto.Birthday,
                    Address = StaffDto.Address,
                    Email = StaffDto.Email,
                    Gender = StaffDto.Gender,
                    PhoneNumber = StaffDto.PhoneNumber,
                    Salary = StaffDto.Salary,
                    SelectedWorkShifts = new List<SelectedWorkShift>()
                };

                foreach (var WorkShift in StaffDto.WorkShifts)
                {
                    var workShift = await _context.WorkShifts.SingleOrDefaultAsync(e => e.WorkShift1 == WorkShift);
                    if (workShift != null)
                    {
                        string idSelectedWorkShift = Guid.NewGuid().ToString().Substring(0, 10);
                        var selectedWorkShift = new SelectedWorkShift
                        {
                            IdSeletedWorkShift = idSelectedWorkShift,
                            IdStaff = IdStaff,
                            IdWorkShift = workShift.IdWorkShift
                        };

                        staff.SelectedWorkShifts.Add(selectedWorkShift);
                        _context.SelectedWorkShifts.Add(selectedWorkShift);
                    }
                }
                _context.staff.Add(staff);
                await _context.SaveChangesAsync();
                response.Data = staff;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<staff>> UpdateInfoStaff(string Id, StaffCreateDto staffDto)
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
                staff dbStaff = await _context.staff.FindAsync(Id);
                if(dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                _context.staff.Remove(dbStaff);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<staff>>> SearchStaffByName(string staffName)
        {
            var response = new ApiResponse<List<staff>>();
            try
            {
                var dbStaffs = await _context.staff
                 .Where(c => c.Fullname != null && c.Fullname.Contains(staffName))
                 .ToListAsync();
                response.Data = dbStaffs;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<staff>> SearchStaffByPhone(string staffPhone)
        {
            var response = new ApiResponse<staff>();
            try
            {
                var dbStaff = await _context.staff.FirstOrDefaultAsync(c => c.PhoneNumber != null && c.PhoneNumber.Equals(staffPhone));
                if (dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
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

    }
}
