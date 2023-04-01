using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;

namespace QuanLyQuanCafe.Services.TableFoodServices
{
    public interface ITableFoodService
    {
        Task<ApiResponse<List<TableFood>>> GetAllTableFood();
        Task<ApiResponse<TableFood>> GetTableFoodById(string Id);
        Task<ApiResponse<TableFood>> CreateTableFood(TableFoodDto TableFoodDto);

        Task<ApiResponse<TableFood>> UpdateTableFood(string Id, TableFoodDto TableFoodDto);
        Task<ApiResponse<AnyType>> DeleteTableFood(string Id);
    }
}
