using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.StaffServices;
using AutoMapper;

namespace QuanLyQuanCafe.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly CafeContext _context;
        private readonly IStaffService _staffService;
        private readonly IMapper _mapper;
        public StaffController(CafeContext _context,IMapper mapper, IStaffService staffService)
        {
            this._context = _context;
            this._staffService = staffService;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                var response = await _staffService.GetAllStaff();
                return Ok(response);
               
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("CreateStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffDto StaffDto) 
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var dbStaff = _context.staff.Where(u=> u.IdStaff == Id).FirstOrDefault();
                if(dbStaff != null) {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "staff already exist" });
                }
                var staff = new staff
                {
                    IdStaff= Id,
                    Fullname = StaffDto.Fullname,
                    Birthday = StaffDto.Birthday,
                    Address = StaffDto.Address,
                    Email = StaffDto.Email,
                    Gender = StaffDto.Gender,
                    PhoneNumber = StaffDto.PhoneNumber,
                    Salary = StaffDto.Salary,
                };
                _context.staff.Add(staff);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "Create success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status= false, Message = ex.Message});
            }
        }
        [HttpPatch]
        [Route("UpdateInfoStaff/{Id}")]
        public async Task<IActionResult> UpdateInfoStaff(string Id)
        {
            try
            {
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "update success" });

            }catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status= false , Message = ex.Message});
            }
        }
        [HttpDelete]
        [Route("DeleteStaff/{Id}")]
        public async Task<IActionResult> DeleteStaff(string Id)
        {
            try
            {
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "delete success" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getStaff/{Id}")]
        public async Task<IActionResult> getStaff(string Id)
        {
            try
            {
                var response = await _staffService.getStaff(Id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false , Message = ex.Message});
            }
        }
    }
}
