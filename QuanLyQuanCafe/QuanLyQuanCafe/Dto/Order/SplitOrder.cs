namespace QuanLyQuanCafe.Dto.Order
{
    public class SplitOrder
    {
        public string? IdOldOrder { get; set; } 
        public string? IdNewOrder { get; set; } 
        public List<SplitOrder>? SplitOrders { get; set; }

    }
}
