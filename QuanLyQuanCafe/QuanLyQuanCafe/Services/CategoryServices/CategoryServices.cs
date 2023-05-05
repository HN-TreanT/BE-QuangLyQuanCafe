using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Category;
using QuanLyQuanCafe.Services.ProductServices;

namespace QuanLyQuanCafe.Services.CategoryServices
{
    public class CategoryServices:ICategoryService
    {
        private readonly  CafeContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public CategoryServices(CafeContext context, IMapper mapper,IProductService productService)
        {
            this._context = context;
            this._mapper = mapper;
            this._productService = productService;
        }
        public async Task<ApiResponse<List<Category>>> GetAllCategory()
        {
            var response = new ApiResponse<List<Category>>();
            var DbCategories = await _context.Categories.Include(p => p.Products.Where(p=> p.Status == 1))                                                 
                                                       .ToListAsync();
            response.Data = DbCategories;
            return response;
        }
        public async Task<ApiResponse<Category>> GetCategoryById(string Id)
        {
            var response = new ApiResponse<Category>();
            /*var dbCategory = await _context.Categories.Include(p => p.Products.Where(p => p.Status == 1))
                                          .SingleOrDefaultAsync(c => c.IdCategory == Id);*/
            var dbCategory = await _context.Categories.Include(p => p.Products)
                                         .SingleOrDefaultAsync(c => c.IdCategory == Id);
            if (dbCategory == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbCategory;
            return response;
        }

        public async Task<ApiResponse<Category>> CreateCategory(CategoryDto categoryDto)
        {
               var response = new ApiResponse<Category>();          
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var category = new Category
                {
                    IdCategory = Id,
                    Name = categoryDto.Name,
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                response.Data = category;
                return response;
        }

        public async Task<ApiResponse<Category>> UpdateCategory(string Id, CategoryDto categoryDto)
        {
            var response = new ApiResponse<Category>();
           
                var dbCategory = await _context.Categories.SingleOrDefaultAsync(c => c.IdCategory == Id);
                if (dbCategory == null)
                {
                    response.Status = false;
                    response.Message = "Not found category";
                    return response;
                }
                _mapper.Map(categoryDto, dbCategory);
                _context.Categories.Update(dbCategory);
                await _context.SaveChangesAsync();
                response.Data = dbCategory;          
            return response;

        }

        public async Task<ApiResponse<Category>> DeleteCategory(string Id)
        {
                var response = new ApiResponse<Category>();    
                var dbCategory = await _context.Categories.Include(c=> c.Products)
                                                          .SingleOrDefaultAsync(c => c.IdCategory == Id); 
                          
                if (dbCategory == null)
                {
                   response.Status =false;
                    response.Message = "Not found category";
                    return response;
                }
              _context.Products.RemoveRange(dbCategory.Products);
               _context.Categories.Remove(dbCategory);
                await _context.SaveChangesAsync();         
            return response;

        }
    }
}
