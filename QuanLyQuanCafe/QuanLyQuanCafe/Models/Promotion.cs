using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionProducts = new HashSet<PromotionProduct>();
        }

        public string IdPromotion { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}
