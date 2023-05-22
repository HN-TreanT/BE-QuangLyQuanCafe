using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.StaffServices;
using AutoMapper;
using QuanLyQuanCafe.Dto.Staff;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStaff(int page,string? name)
        {
            try
            {
                var response = await _staffService.GetAllStaff(page,name);
                return Ok(response);
               
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("CreateStaff")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchStaffByName(string staffName)
        {
            try
            {
                var response = await _staffService.SearchStaffByName(staffName);
                if(response.Data.Count <= 0)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "Not found" });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("searchStaffByPhone/{staffPhone}")]
        [Authorize(Roles = "Admin")]
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
        [HttpPost]
        [Route("UploadAvartarStaff")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadAvartarStaff( [FromForm] ModelAvartarImage avartarStaffImage)
        {
            try
            {              
                var response =await _staffService.UploadAvartarStaff(avartarStaffImage.IdStaff,avartarStaffImage.file);
                return Ok(response);
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("StaffImage/{imageName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StaffImage", imageName);
            Console.WriteLine(imagePath);
            var imageStream = System.IO.File.OpenRead(imagePath);
            return File(imageStream, "image/jpeg");

        }

        [HttpGet]
        [Route("searchStaffByEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchStaffByEmail(string email)
        {
            try
            {
                var response = await _staffService.searchStaffbyEmail(email);
                return Ok(response);

            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
