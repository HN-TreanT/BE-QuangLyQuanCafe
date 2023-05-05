using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/*using CoreApiResponse;*/
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using System.Net;
//using Cafe.helper;
using Microsoft.OpenApi.Any;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyQuanCafe.Controllers
{
    public class LoginInfo
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class RegisterInfo
    {
        public string? DisplayName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class ChangePasswordRequest
    {
        public string? Username { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CafeContext _context;
        public AccountController(CafeContext _context)
        {
            this._context = _context;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginInfo account)
        {
            try
            {
                string password = PasswordTool.HashPassword(account.Password);
                var dbAccount = _context.Accounts.Where(u => u.Username == account.Username && u.Password == password).Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Active
                }).FirstOrDefault();
                if (dbAccount == null)
                {

                    return BadRequest(new ApiResponse<AnyType> { Status = false, Message = "user or password incorrect" });
                }

                var response = new ApiResponse<object> { Status = true, Message = "success", Data = dbAccount };
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }

        }

        private IActionResult Content(HttpStatusCode oK, object value)
        {
            throw new NotImplementedException();
        }

        private IActionResult CustomResult(string v, object dbAccount)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Regiteration([FromBody] RegisterInfo register)
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0,10);
                string hashPassword = PasswordTool.HashPassword(register.Password);
                var dbAccount = _context.Accounts.Where(u => u.Username == register.Username).FirstOrDefault();
                if (dbAccount != null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "username already exists" });
                }

                var account = new Account
                {
                    Id = Id,
                    DisplayName = register.DisplayName,
                    Username = register.Username,
                    Password = hashPassword
                };
                    _context.Accounts.Add(account);
                   await _context.SaveChangesAsync();

                return Ok(new ApiResponse<object> { Status = true, Message = "user is successfully registered" });
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }

        }
        [HttpPatch]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var dbAccount = _context.Accounts.Where(u => u.Username == request.Username).FirstOrDefault();
                //check account exist 
                if(dbAccount == null)
                {
                    return NotFound(new ApiResponse<object> { Status = false, Message = " Account not exist" });
                }
                //check if old password incorrect
                if (!PasswordTool.VerifyPassword(request.OldPassword, dbAccount.Password))
                {
                    return BadRequest(new ApiResponse<AnyType> { Status = false, Message = "Password incorrect" });
                }

                dbAccount.Password = PasswordTool.HashPassword(request.NewPassword);
                _context.Update(dbAccount);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<object> { Status = true, Message = "Change password succes", Data = dbAccount });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message});
            }
        }
        [HttpGet]
        [Route("getAccountById/{Id}")]
        public async Task<IActionResult> getUser( string Id )
        {
            try
            {
                var dbAccount = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == Id);
                if (dbAccount == null)
                {
                    return NotFound(new ApiResponse<AnyType> { Status = false, Message = "Not found user" });
                }
                else
                {
                    return Ok(new ApiResponse<Account> { Status = true, Message = "success", Data = dbAccount });
                }

            }
            catch(Exception e)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("deleteAccount/{Id}")]
        public async Task<IActionResult> deleteUser(string Id)
        {
            try
            {
                var dbAccount =await _context.Accounts.FindAsync(Id);
                if(dbAccount == null)
                {
                    return NotFound(new ApiResponse<AnyType> { Status = false, Message = "Not found user" });
                }
                _context.Accounts.Remove(dbAccount);
                await _context.SaveChangesAsync();  

                return Ok(new ApiResponse<AnyType> { Status = true, Message = "success delete" });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllAccount")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var dbAccounts = await _context.Accounts.ToListAsync();
                return Ok(new ApiResponse<List<Account>> { Status = true, Message = "Success", Data = dbAccounts });

            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status= false, Message = ex.Message });
            }
        }
    }

    internal class APIResponse
    {
        public APIResponse()
        {
        }

        internal IActionResult ResponseSuccess()
        {
            throw new NotImplementedException();
        }
    }
}
