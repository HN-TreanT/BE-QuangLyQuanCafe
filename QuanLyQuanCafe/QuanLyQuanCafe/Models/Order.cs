using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string IdOrder { get; set; } = null!;
        public string? IdCustomer { get; set; }
        public long? Price { get; set; }
        public string? IdTable { get; set; }
        public DateTime? TimePay { get; set; }
        public byte? Status { get; set; }
        public int? Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Customer? IdCustomerNavigation { get; set; }
        public virtual TableFood? IdTableNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
