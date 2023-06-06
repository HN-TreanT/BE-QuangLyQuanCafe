using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Staff;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using QuanLyQuanCafe.Dto.Customer;

namespace QuanLyQuanCafe.Services.StaffServices
{
    public class StaffServices:IStaffService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        readonly string coverImageFolderPath = string.Empty;
        public static int PAGE_SIZE { get; set; } = 5;
        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
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

        public async Task<ApiResponse<List<staff>>> GetAllStaff(int page,string? name)
        {
            var response = new ApiResponse<List<staff>>();
            if( string.IsNullOrEmpty(name))
            {
                var dbStaffs = await _context.staff.Include(s => s.SelectedWorkShifts).OrderByDescending(st=> st.CreatedAt)
                    .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                    .ToListAsync();
                if (dbStaffs.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.TotalPage = _context.staff.Count();
                response.Data = dbStaffs;  
            }             
            if(name != null)
            {
                var searchValue = ConvertToUnSign(name);
                var query = _context.staff.Include(p => p.SelectedWorkShifts).Where(delegate (staff c)
                    {
                        if (ConvertToUnSign(c.Fullname).IndexOf(searchValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                response.TotalPage = query.ToList().Count();
                response.Data = query.OrderByDescending(st=> st.CreatedAt).Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            }
            return response;
        }

        public async Task<ApiResponse<staff>> getStaff(string Id)
        {
            var response = new ApiResponse<staff>();          
                var dbStaff = await _context.staff.Include(s => s.SelectedWorkShifts)
                .SingleOrDefaultAsync(s => s.IdStaff == Id);
                if (dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "not found ";
                    return response;
                }
                response.Data = dbStaff;
            return response;
        }

        public async Task<ApiResponse<staff>> CreateStaff(StaffCreateDto StaffDto)
        {
            var response = new ApiResponse<staff>();          
                string IdStaff = Guid.NewGuid().ToString().Substring(0, 10);
                var dbStaff = _context.staff.Where(u => u.Email == StaffDto.Email).FirstOrDefault();
               
                if (dbStaff != null)
                {
                    response.Status = false;
                    response.Message = "email already";
                    return response;
                }
            if (StaffDto.PhoneNumber != null)
            {
                if (StaffDto.PhoneNumber.Length < 10 || StaffDto.PhoneNumber.Length > 11)
                {
                    response.Status = false;
                    response.Message = "Số điện thoại không hợp lệ!";
                    return response;
                }

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
            return response;
        }

        public async Task<ApiResponse<staff>> UpdateInfoStaff(string Id, StaffCreateDto staffDto)
        {
            var response = new ApiResponse<staff>();          
                var dbStaff = await _context.staff.Include(s => s.SelectedWorkShifts).SingleOrDefaultAsync(st => st.IdStaff == Id);
                if(dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
            if (staffDto.PhoneNumber != null)
            {
                if (staffDto.PhoneNumber.Length < 10 || staffDto.PhoneNumber.Length > 11)
                {
                    response.Status = false;
                    response.Message = "Số điện thoại không hợp lệ!";
                    return response;
                }

            }
            //update SelectedWorkShift
            if (staffDto.WorkShifts != null)
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
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteStaff(string Id)
        {
            var response = new ApiResponse<AnyType>();         
                var dbStaff = await _context.staff.FindAsync(Id);
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
            return response;
        }

         public async Task<ApiResponse<List<staff>>> SearchStaffByName(string staffName)
        {
            var response = new ApiResponse<List<staff>>();
            var dbStaffs = _context.staff.AsEnumerable()
                                       .Where(m => _Convert.ConvertToUnSign(m.Fullname).Contains(_Convert.ConvertToUnSign(staffName)))
                                       .ToList();
            
                response.Data = dbStaffs;         
            return response;
        }

        public async Task<ApiResponse<staff>> SearchStaffByPhone(string staffPhone)
        {
            var response = new ApiResponse<staff>();          
                var dbStaff = await _context.staff.FirstOrDefaultAsync(c => c.PhoneNumber != null && c.PhoneNumber.Equals(staffPhone));
                if (dbStaff == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbStaff;        
            return response;
        }

        public async Task<ApiResponse<staff>> UploadAvartarStaff(string IdStaff, IFormFile file)
        {

            var response = new ApiResponse<staff>();         
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
            return response;
        }


        public async Task<ApiResponse<staff>> searchStaffbyEmail(string email)
        {
            var response = new ApiResponse<staff>();
            var dbStaff = await _context.staff.SingleOrDefaultAsync(s=>s.Email == email);
            if(dbStaff == null)
            {
                response.Status = false;
                response.Message = "not found staff";
                return response;
            }
            response.Data = dbStaff;    
            return response;
        }
    }
}
