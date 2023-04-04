using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Staff;
using System.Linq;

namespace QuanLyQuanCafe.Services.StaffServices
{
    public class StaffServices:IStaffService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        readonly string coverImageFolderPath = string.Empty;
        public StaffServices(CafeContext context, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            this._context = context;
            this._mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            coverImageFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "StaffPic/");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }

        }

        public async Task<ApiResponse<List<staff>>> GetAllStaff()
        {
            var response = new ApiResponse<List<staff>>();
            try
            {
                var dbStaffs = await _context.staff.Include(s => s.SelectedWorkShifts).ToListAsync();
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
                    var workShift = await _context.WorkShifts.SingleOrDefaultAsync(e => e.IdWorkShift == WorkShift);
                    if (workShift != null)
                    {
                        string idSelectedWorkShift = Guid.NewGuid().ToString().Substring(0, 10);
                        var selectedWorkShift = new SelectedWorkShift
                        {
                            IdSeletedWorkShift = idSelectedWorkShift,
                            IdStaff = IdStaff,
                            IdWorkShift = WorkShift,
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
                var dbStaff = await _context.staff.Include(s => s.SelectedWorkShifts).SingleOrDefaultAsync(st => st.IdStaff == Id);
                if(dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
                //update SelectedWorkShift
                if(staffDto.WorkShifts != null)
                {
                    var workShiftsToRemove = dbStaff.SelectedWorkShifts
                   .Where(selectedWS => staffDto.WorkShifts.All(ws => ws != (int)selectedWS.IdWorkShift)).ToList();
                    var workShiftsToAdd = staffDto.WorkShifts
                    .Except(dbStaff.SelectedWorkShifts.Select(selectedWS => (int)selectedWS.IdWorkShift));
                   
                    foreach (var ws in workShiftsToAdd)
                    {
                        var workShift = await _context.WorkShifts.SingleOrDefaultAsync(e => e.IdWorkShift == ws);
                        if (workShift != null)
                        {
                            string idSelectedWorkShift = Guid.NewGuid().ToString().Substring(0, 10);
                            var selectedWorkShift = new SelectedWorkShift
                            {
                                IdSeletedWorkShift = idSelectedWorkShift,
                                IdStaff = dbStaff.IdStaff,
                                IdWorkShift = ws,
                            };

                            dbStaff.SelectedWorkShifts.Add(selectedWorkShift);
                            _context.SelectedWorkShifts.Add(selectedWorkShift);
                        }

                    }
                    foreach (var selected in workShiftsToRemove)
                    {
                        dbStaff.SelectedWorkShifts.Remove(selected);
                    }
                    _context.SelectedWorkShifts.RemoveRange(workShiftsToRemove);
                }
                /////////////////////////////////////////

                _mapper.Map(staffDto, dbStaff);
                _context.staff.Update(dbStaff);
                await _context.SaveChangesAsync();
                response.Data = dbStaff;

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
                var ListSelectedWS = _context.SelectedWorkShifts
                                      .Where(sws => sws.IdStaff == dbStaff.IdStaff);
                _context.SelectedWorkShifts.RemoveRange(ListSelectedWS);
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
                if(dbStaffs.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "not found";
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

        public async Task<ApiResponse<staff>> UploadAvartarStaff(string IdStaff, IFormFile file)
        {

            var response = new ApiResponse<staff>();
            try {
                var dbStaff = await _context.staff.FindAsync(IdStaff);
                
                if(dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "not found staff";
                    return response;
                }
                if (dbStaff.PathImage != null)
                {                  
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", dbStaff.PathImage);
                    File.Delete(path);
                }
                var special = Guid.NewGuid().ToString();
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\StaffImage", special + "-" + file.FileName);
                using (FileStream ms = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(ms);
                }
                var pathImage = Path.Combine( "StaffImage", special + "-" + file.FileName);
                dbStaff.PathImage = pathImage;
                await _context.SaveChangesAsync();
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
