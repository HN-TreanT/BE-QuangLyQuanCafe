namespace QuanLyQuanCafe.Dto.Material
{
    public class MaterialDto
    {
        public string NameMaterial { get; set; } = null!; 
        public string? Description { get; set; }
        public float? Amount { get; set; }  
        public string? Unit { get; set; }
        public int? Expiry { get; set; }
    }
}
