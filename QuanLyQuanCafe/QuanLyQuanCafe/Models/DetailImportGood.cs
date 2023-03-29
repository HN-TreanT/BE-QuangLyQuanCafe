﻿using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class DetailImportGood
    {
        public string IdDetailImportGoods { get; set; } = null!;
        public string? IdImportGoods { get; set; }
        public string? IdProduct { get; set; }
        public int? Amount { get; set; }
        public double? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ImportGood? IdImportGoodsNavigation { get; set; }
        public virtual Product? IdProductNavigation { get; set; }
    }
}
