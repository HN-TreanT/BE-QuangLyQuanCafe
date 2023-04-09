
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace QuanLyQuanCafe.Tools
{
    public class _Convert
    {
        public static string ConvertToUnSign(string s)

        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();

        }
    }
}
