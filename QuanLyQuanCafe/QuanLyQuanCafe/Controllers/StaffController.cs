using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.StaffServices;
using AutoMapper;
using QuanLyQuanCafe.Dto.Staff;
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
        public async Task<IActionResult> CreateStaff([FromBody] StaffCreateDto StaffDto) 
        {
            try
            {
                var res = await _staffService.CreateStaff(StaffDto);    
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateInfoStaff/{Id}")]
        public async Task<IActionResult> UpdateInfoStaff(string Id, [FromBody] StaffCreateDto staffDto)
        {
            try
            {
                var response = await _staffService.UpdateInfoStaff(Id, staffDto);
                
                return Ok(response);

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
               var response = await _staffService.DeleteStaff(Id);
                return Ok(response);

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

        [HttpGet]
        [Route("searchStaffByName/{staffName}")]
        public async Task<IActionResult> SearchStaffByName(string staffName)
        {
            try
            {
                var response = await _staffService.SearchStaffByName(staffName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("searchStaffByPhone/{staffPhone}")]
        public async Task<IActionResult> SearchStaffByPhone(string staffPhone)
        {
            try
            {
                var response = await _staffService.SearchStaffByPhone(staffPhone);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
