using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class staff
    {
        public staff()
        {
            SelectedWorkShifts = new HashSet<SelectedWorkShift>();
        }

        public string IdStaff { get; set; } = null!;
        public string? Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double? Salary { get; set; }
        public string? PathImage { get; set; }

        public virtual ICollection<SelectedWorkShift> SelectedWorkShifts { get; set; }
    }
}
