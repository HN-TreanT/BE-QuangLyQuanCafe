using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string IdCustomer { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
