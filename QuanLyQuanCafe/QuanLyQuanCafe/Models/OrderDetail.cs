using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class OrderDetail
    {
        public string IdOrderDetail { get; set; } = null!;
        public string? IdOrder { get; set; }
        public string? IdProduct { get; set; }
        public double? Price { get; set; }
        public int? Amout { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Order? IdOrderNavigation { get; set; }
        public virtual Product? IdProductNavigation { get; set; }
    }
}
