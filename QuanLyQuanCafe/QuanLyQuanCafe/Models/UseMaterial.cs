using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class UseMaterial
    {
        public string IdUseMaterial { get; set; } = null!;
        public string? IdProduct { get; set; }
        public string? IdMaterial { get; set; }
        public double? Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Material? IdMaterialNavigation { get; set; }
        public virtual Product? IdProductNavigation { get; set; }
    }
}
