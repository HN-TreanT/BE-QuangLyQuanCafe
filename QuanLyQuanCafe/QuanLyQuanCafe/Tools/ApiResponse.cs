namespace QuanLyQuanCafe.Tools
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; } = true;
        public int? TotalPage { get; set; }
        public string Message { get; set; } = "Success";
        public T Data { get; set; }
       
    }
}
