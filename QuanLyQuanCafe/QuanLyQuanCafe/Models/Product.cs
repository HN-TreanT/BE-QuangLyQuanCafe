using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Product
    {
        public Product()
        {
            DetailImportGoods = new HashSet<DetailImportGood>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string IdProduct { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Unit { get; set; }
        public string? IdCategory { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Category? IdCategoryNavigation { get; set; }
        public virtual ICollection<DetailImportGood> DetailImportGoods { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
