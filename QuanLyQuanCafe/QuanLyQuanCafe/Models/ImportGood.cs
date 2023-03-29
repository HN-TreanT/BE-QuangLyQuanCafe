using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class ImportGood
    {
        public ImportGood()
        {
            DetailImportGoods = new HashSet<DetailImportGood>();
        }

        public string IdImportGoods { get; set; } = null!;
        public string? IdProvider { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Provider? IdProviderNavigation { get; set; }
        public virtual ICollection<DetailImportGood> DetailImportGoods { get; set; }
    }
}
