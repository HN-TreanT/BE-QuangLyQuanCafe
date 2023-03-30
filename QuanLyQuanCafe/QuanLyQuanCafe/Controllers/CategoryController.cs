using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe.Controllers
{
    public class categoryInfo
    {
        public string Name { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public CategoryController(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("getAllCategory")]
        public async Task<IActionResult> getAllCategory()
        {
            try
            {
                var DbCategories = await _context.Categories.ToListAsync();
                if(DbCategories.Count <= 0)
                {
                    return NotFound();
                }
                return Ok(new ApiResponse<List<Category>> { Status = true, Message = "Success",Data = DbCategories });

            } catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCategoryById/{Id}")]
        public async Task<IActionResult> getCategoryById(string Id)
        {
            try
            {
                var dbCategory = await _context.Categories.SingleOrDefaultAsync(c => c.IdCategory == Id);
                if(dbCategory == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "Not found " });
                }
                return Ok(new ApiResponse<Category> { Status = true, Message = "Success", Data = dbCategory});

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createCategory")]
        public async Task<IActionResult> getCategoryById([FromBody] categoryInfo categoryInfo)
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0,10);
                var category = new Category
                {
                    IdCategory = Id,
                    Name = categoryInfo.Name,
                };
                _context.Categories.Add(category); 
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<Category> { Status = true, Message = "Success",Data = category });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateCategory/{Id}")]
        public async Task<IActionResult> UpdateCategory(string Id, [FromBody] categoryInfo categoryInfo)
        {
            try
            {
                var dbCategory = await _context.Categories.SingleOrDefaultAsync(c => c.IdCategory == Id);
                if(dbCategory == null)
                {
                    return NotFound();
                }
                _mapper.Map(categoryInfo, dbCategory);
                _context.Categories.Update(dbCategory);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<Category> { Status = true, Message = "Success",Data = dbCategory });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("deleteCategory/{Id}")]
        public async Task<IActionResult> DeleteCategory(string Id)
        {
            try
            {
                var dbCategory = await _context.Categories.SingleOrDefaultAsync(c => c.IdCategory == Id);
                if (dbCategory == null)
                {
                    return NotFound();
                }
                _context.Categories.Remove(dbCategory);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "Success" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
    
