using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanCafe.Models
{
    public partial class WorkShift
    {
        public WorkShift()
        {
            SelectedWorkShifts = new HashSet<SelectedWorkShift>();
        }

        public int IdWorkShift { get; set; }
        [Required]
        public int? WorkShift1 { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? TimeOn { get; set; }

        public virtual ICollection<SelectedWorkShift> SelectedWorkShifts { get; set; }
    }
}
