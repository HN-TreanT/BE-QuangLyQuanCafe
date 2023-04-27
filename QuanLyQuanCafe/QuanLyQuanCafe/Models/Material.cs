using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Material
    {
        public Material()
        {
            DetailImportGoods = new HashSet<DetailImportGood>();
            UseMaterials = new HashSet<UseMaterial>();
        }

        public string IdMaterial { get; set; } = null!;
        public string NameMaterial { get; set; } = null!;
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public string? Unit { get; set; }
        public int? Expiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<DetailImportGood> DetailImportGoods { get; set; }
        public virtual ICollection<UseMaterial> UseMaterials { get; set; }
    }
}
