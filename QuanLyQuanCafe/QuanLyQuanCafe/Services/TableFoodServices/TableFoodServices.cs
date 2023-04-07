using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.TableFood;

namespace QuanLyQuanCafe.Services.TableFoodServices
{
    public class TableFoodServices:ITableFoodService
    {

        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public TableFoodServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<List<TableFood>>> GetAllTableFood()
        {
            var response = new ApiResponse<List<TableFood>>(); 
                var dbTableFoods = await _context.TableFoods.ToListAsync();
                if (dbTableFoods.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbTableFoods;          
            return response;
        }

        public async Task<ApiResponse<TableFood>> GetTableFoodById(string Id)
        {
            var response = new ApiResponse<TableFood>();        
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if (dbTableFood == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
               response.Data = dbTableFood;
            return response;
        }

        public async Task<ApiResponse<TableFood>> CreateTableFood(TableFoodDto TableFoodDto)
        {
            var response = new ApiResponse<TableFood>();   
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var tableFood = new TableFood
                {
                    IdTable = Id,
                    Name = TableFoodDto.Name,
                    Status = TableFoodDto.Status,
                };
                _context.TableFoods.Add(tableFood);
                await _context.SaveChangesAsync();
                response.Data = tableFood;         
            return response;
        }

        public async Task<ApiResponse<TableFood>> UpdateTableFood(string Id, TableFoodDto TableFoodDto)
        {
            var response = new ApiResponse<TableFood>();
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if (dbTableFood == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
                _mapper.Map(TableFoodDto, dbTableFood);
                _context.TableFoods.Update(dbTableFood);
                await _context.SaveChangesAsync();
                response.Data = dbTableFood;           
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteTableFood(string Id)
        {
            var response = new ApiResponse<AnyType>();        
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if (dbTableFood == null)
                {
                    response.Status =false;
                    response.Message = "not found";
                    return response;
                }
                _context.TableFoods.Remove(dbTableFood);
                await _context.SaveChangesAsync();            
            return response;
        }
    }
}
