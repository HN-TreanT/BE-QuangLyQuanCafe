namespace QuanLyQuanCafe.Dto.Staff
{
    public class StaffCreateDto
    {
        public string? Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }  
        public string? PhoneNumber { get; set; }
        public float? Salary { get; set; }
        public List<int>? WorkShifts { get; set; } 

    }
}
