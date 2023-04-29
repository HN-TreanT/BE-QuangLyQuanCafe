using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class DetailImportGood
    {
        public string IdDetailImportGoods { get; set; } = null!;
        public string? IdMaterial { get; set; }
        public string? PhoneProvider { get; set; }
        public string? NameProvider { get; set; }
        public int? Amount { get; set; }
        public double? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Material? IdMaterialNavigation { get; set; }
    }
}
