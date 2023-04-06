using QuanLyQuanCafe.Dto.TokenAuth;
using System.Security.Claims;

namespace QuanLyQuanCafe.Services.TokenServices
{
    public interface ITokenService
    {
        TokenResponse GetToken(IEnumerable<Claim> claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
