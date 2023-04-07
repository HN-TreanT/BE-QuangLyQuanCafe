namespace QuanLyQuanCafe.Dto.Product
{
    public class ProductDto
    {
        public string? Title { get; set; } 
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Unit { get; set; }
        public string? IdCategory { get; set; }
        public IFormFile? file { get; set; } 
    }
}
