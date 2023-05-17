using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.ProductServices
{
    public interface IProductService
    {
        Task<ApiResponse<Product>> GetProductById(string Id);
        Task<ApiResponse<List<Product>>> GetAllProduct(int pageSize,int page,string? typeSearch,string? searchValue);
        Task<ApiResponse<List<Product>>> GetAllProductByIdCategory(int pageSize, int page, string Id, string? searchValue);
        Task<ApiResponse<Product>> CreatePoduct(ProductDto productDto);
        Task<ApiResponse<Product>> UpdatePoduct(string Id,ProductDto productDto);
        Task<ApiResponse<AnyType>> DeletePoduct(string Id);
        Task<ApiResponse<List<ProductOrderStatistic>>> GetBestSellProduct(int time);
    }
}
