using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class SelectedWorkShift
    {
        public string IdSelectedWs { get; set; } = null!;
        public string? IdStaff { get; set; }
        public int? IdWorkShift { get; set; }

        public virtual staff? IdStaffNavigation { get; set; }
        public virtual WorkShift? IdWorkShiftNavigation { get; set; }
    }
}
