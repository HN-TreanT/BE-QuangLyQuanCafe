using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.PProduct;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.PProductServices
{
    public interface IPProductService
    {
        Task<ApiResponse<PromotionProduct>> GetPPById(string id);
        Task<ApiResponse<List<PromotionProduct>>> GetAllPP();
        Task<ApiResponse<PromotionProduct>> CreatePP(PromotionProductDto ppDto);
        Task<ApiResponse<PromotionProduct>> UpdatePP(string Id, PromotionProductDto ppDto);
        Task<ApiResponse<AnyType>> DeletePP(string Id); 

    }
}
