namespace QuanLyQuanCafe.Dto.Order
{
    public class OrderGet
    {
        public string? IdOrder { get; set; }
        public byte? Status { get; set; }
        public int? Amount { get; set; }
        public DateTime? TimePay { get; set; }  
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public string? IdCustomer { get; set; }
        public string? Fullname { get; set; }    
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set;}
        public string? IdTable { get; set; }
        public int? NameTable { get; set; }
        public byte? StatusTable { get; set; }
       
    }
}
