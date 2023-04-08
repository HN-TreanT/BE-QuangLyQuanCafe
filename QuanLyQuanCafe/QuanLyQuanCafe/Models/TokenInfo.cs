using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class TokenInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
