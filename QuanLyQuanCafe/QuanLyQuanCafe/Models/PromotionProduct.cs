using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class PromotionProduct
    {
        public string IdPp { get; set; } = null!;
        public string? IdPromotion { get; set; }
        public string? IdProduct { get; set; }
        public int? MinCount { get; set; }
        public double? Sale { get; set; }

        public virtual Product? IdProductNavigation { get; set; }
        public virtual Promotion? IdPromotionNavigation { get; set; }
    }
}
