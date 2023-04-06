using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanCafe.Dto.TokenAuth
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }


    }
}
