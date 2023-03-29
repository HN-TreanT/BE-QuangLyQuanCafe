using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe.Controllers
{
    public class StaffInfo
    {
        public string Fullname { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public float Salary { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly CafeContext _context;
        public StaffController(CafeContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        [Route("GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message});
            }
        }
        [HttpPost]
        [Route("CreateStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffInfo staffInfo) 
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
                    Fullname = staffInfo.Fullname,
                    Birthday = staffInfo.Birthday,
                    Address = staffInfo.Address,
                    Email = staffInfo.Email,
                    Gender = staffInfo.Gender,
                    PhoneNumber = staffInfo.PhoneNumber,
                    Salary = staffInfo.Salary,
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
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "success" });
            }catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false , Message = ex.Message});
            }
        }
    }
}
