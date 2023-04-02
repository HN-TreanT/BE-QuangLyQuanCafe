using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Dto.Category;

namespace QuanLyQuanCafe.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<Category>>> GetAllCategory();
        Task<ApiResponse<Category>> GetCategoryById(string Id);
        Task<ApiResponse<Category>> CreateCategory(CategoryDto categoryDto);
        Task<ApiResponse<Category>> UpdateCategory(string Id, CategoryDto categoryDto);
        Task<ApiResponse<Category>> DeleteCategory(string Id);


    }
}
