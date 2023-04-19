using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.UseMaterial;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.UseMaterialServices
{
    public interface IUseMaterialServices
    {
        Task<ApiResponse<UseMaterial>> GetUseMaterialById(string Id);
        Task<ApiResponse<List<UseMaterial>>> GetAllUseMaterial();
        Task<ApiResponse<UseMaterial>> CreateUseMaterial(UseMaterialDto useMaterialDto);
        Task<ApiResponse<UseMaterial>> UpdateUseMaterial(string Id,UseMaterialDto useMaterialDto);
        Task<ApiResponse<AnyType>> DeleteUseMaterial(string Id);
        Task<ApiResponse<List<UseMaterial>>>  CreateManyUseMaterial(List<UseMaterialDto> useMaterialDtos);
        Task<ApiResponse<AnyType>> DeleteAllUseMaterialByIdProduct(string IdProduct);
    }
}
