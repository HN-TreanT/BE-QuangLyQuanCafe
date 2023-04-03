using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class SelectedWorkShift
    {
        public string? IdStaff { get; set; }
        public int? IdWorkShift { get; set; }
        public string IdSeletedWorkShift { get; set; } = null!;

        public virtual staff? IdStaffNavigation { get; set; }
        public virtual WorkShift? IdWorkShiftNavigation { get; set; }
    }
}
