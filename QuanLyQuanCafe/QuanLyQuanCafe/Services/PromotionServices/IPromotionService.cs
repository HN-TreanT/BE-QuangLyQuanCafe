using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Promotion;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.PromotionServices
{
    public interface IPromotionService
    {
        Task<ApiResponse<Promotion>> GetPromotionById(string Id);
        Task<ApiResponse<List<Promotion>>> GetAllPromotion();
        Task<ApiResponse<Promotion>> CreatePromotion(PromotionDto promotionDto);
        Task<ApiResponse<Promotion>> UpdatePromotion(string Id,PromotionDto promotionDto);
        Task<ApiResponse<AnyType>> DeletePromotion(string Id);
        Task<ApiResponse<List<Promotion>>> GetPromotionExpired();
    }
}
