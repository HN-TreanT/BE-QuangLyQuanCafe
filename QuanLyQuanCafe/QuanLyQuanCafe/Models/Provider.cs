using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Provider
    {
        public Provider()
        {
            DetailImportGoods = new HashSet<DetailImportGood>();
        }

        public string IdProvider { get; set; } = null!;
        public string? Name { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<DetailImportGood> DetailImportGoods { get; set; }
    }
}
