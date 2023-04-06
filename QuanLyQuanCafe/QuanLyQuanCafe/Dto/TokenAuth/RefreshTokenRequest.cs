namespace QuanLyQuanCafe.Dto.TokenAuth
{
    public class RefreshTokenRequest
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
