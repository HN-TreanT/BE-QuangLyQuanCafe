using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Account
    {
        public string Id { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte? Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
