namespace QuanLyQuanCafe.Models
{
    public class TokenInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }  
    }
}
