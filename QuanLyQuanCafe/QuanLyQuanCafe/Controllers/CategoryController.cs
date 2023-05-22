using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.CategoryServices;
using QuanLyQuanCafe.Dto.Category;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyQuanCafe.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
       
        public CategoryController(CafeContext context, IMapper mapper,ICategoryService categoryService)
        {
            this._context = context;
            this._mapper = mapper;
            this._categoryService = categoryService;    
        }

        [HttpGet]
        [Route("getAllCategory")]
        [Authorize]
        public async Task<IActionResult> getAllCategory()
        {
            try
            {
                var response = await _categoryService.GetAllCategory();
               
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getCategoryById/{Id}")]
        [Authorize]
        public async Task<IActionResult> getCategoryById(string Id)
        {
            try
            {
                var response = await _categoryService.GetCategoryById(Id);
                if(response.Data == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "Not found " });
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getCategoryById([FromBody] CategoryDto CategoryDto)
        {
            try
            {
                var response = await _categoryService.CreateCategory(CategoryDto);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateCategory/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(string Id, [FromBody] CategoryDto CategoryDto)
        {
            try
            {
               var response = await _categoryService.UpdateCategory(Id, CategoryDto);
                return Ok(response);    
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("deleteCategory/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(string Id)
        {
            try
            {
               var response = await _categoryService.DeleteCategory(Id);
                return Ok(response);    

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
    
