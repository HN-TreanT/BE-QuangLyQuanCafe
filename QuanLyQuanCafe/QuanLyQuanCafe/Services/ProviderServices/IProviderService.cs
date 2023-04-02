using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Provider;

namespace QuanLyQuanCafe.Services.ProvideServices
{
    public interface IProviderService
    {
        Task<ApiResponse<List<Provider>>> GetAllProvider();
        Task<ApiResponse<Provider>> GetProviderById(string Id);
        Task<ApiResponse<Provider>> CreateProvider(ProviderDto ProviderDto);

        Task<ApiResponse<Provider>> UpdateProvider(string Id, ProviderDto ProviderDto);
        Task<ApiResponse<AnyType>> Deleteprovider(string Id);
    }
}
