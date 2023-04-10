namespace QuanLyQuanCafe.Dto.Order
{
    public class OrderDto
    {
        public string? IdCustomer { get; set; }
        public string? IdTable { get; set; }
        public int? Amount { get; set; }
        public byte? Status { get; set; }
    }
}
