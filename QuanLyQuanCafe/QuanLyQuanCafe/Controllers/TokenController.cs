using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Dto.TokenAuth;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.TokenServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly CafeContext _ctx;
        private readonly ITokenService _service;
        public TokenController(CafeContext ctx, ITokenService service)
        {
            this._ctx = ctx;
            this._service = service;

        }

        /* [HttpPost]
         public IActionResult Refresh(RefreshTokenRequest tokenApiModel)
         {
             if (tokenApiModel is null)
                 return BadRequest("Invalid client request");
             string accessToken = tokenApiModel.AccessToken;
             string refreshToken = tokenApiModel.RefreshToken;
             var principal = _service.GetPrincipalFromExpiredToken(accessToken);
             var username = principal.Identity.Name;
             var user = _ctx.TokenInfo.SingleOrDefault(u => u.UserName == username);
             if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
                 return BadRequest("Invalid client request");
             var newAccessToken = _service.GetToken(principal.Claims);
             var newRefreshToken = _service.GetRefreshToken();
             user.RefreshToken = newRefreshToken;
             _ctx.SaveChanges();
             return Ok(new RefreshTokenRequest()
             {
                 AccessToken = newAccessToken.TokenString,
                 RefreshToken = newRefreshToken
             });
         }*/
        [HttpPost]
        public IActionResult Refresh(RefreshTokenRequest tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _service.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = _ctx.TokenInfo.SingleOrDefault(u => u.UserName == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
                return BadRequest("Invalid client request");
            var newAccessToken = _service.GetToken(principal.Claims);
            _ctx.SaveChanges();
            return Ok(new RefreshTokenRequest()
            {
                AccessToken = newAccessToken.TokenString,
            });
        }

        //revoken is use for removing token enntry
        [HttpPost,Authorize]
       
        public async Task<IActionResult> Revoke()
        {
            try
            {
               
                var username = User.Identity.Name;
                
                var user =await _ctx.TokenInfo.SingleOrDefaultAsync(u => u.UserName == username);
                if (user == null)
                {
                    return BadRequest();
                }
                user.RefreshToken = "0";
                _ctx.Update(user);   
                await _ctx.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        

    }
}
