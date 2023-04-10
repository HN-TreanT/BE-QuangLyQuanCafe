using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class TableFood
    {
        public TableFood()
        {
            Orders = new HashSet<Order>();
        }

        public string IdTable { get; set; } = null!;
        public int? Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
