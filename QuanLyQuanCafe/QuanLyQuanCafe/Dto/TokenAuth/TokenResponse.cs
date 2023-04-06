namespace QuanLyQuanCafe.Dto.TokenAuth
{
    public class TokenResponse
    {
        public string? TokenString { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
