using System.Security.Cryptography;
using System.Text;

namespace QuanLyQuanCafe.Tools
{
   public class PasswordTool
    {
        public static string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashPassword = sha.ComputeHash(asByteArray);
            
            return Convert.ToBase64String(hashPassword);
        }
        public static bool VerifyPassword(string PasswordRecive, string DbPassword)
        {
            var hashPassword = HashPassword(PasswordRecive);
            if (hashPassword == DbPassword)
            {
                return true;
            }
            else return false;
           
        }
    }
  
}
