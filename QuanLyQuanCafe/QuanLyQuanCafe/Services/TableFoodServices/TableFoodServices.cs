using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.TableFood;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuanLyQuanCafe.Services.TableFoodServices
{
    public class TableFoodServices:ITableFoodService
    {

        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public static int PAGE_SIZE { get; set; } = 18;
        public TableFoodServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<List<TableFood>>> GetAllTableFood(int page,string? stateTable,int? numberTable)
        {
            var response = new ApiResponse<List<TableFood>>();
            var dbTableFoods = new List<TableFood>();
            var count = 0;
            if(stateTable == "emptyTable")
            {
                dbTableFoods = await _context.TableFoods
                    .OrderBy(tb => tb.Name).Where(tb => tb.Status == 0).Where(tb => !numberTable.HasValue || tb.Name == numberTable)
                    .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                    .ToListAsync();
                count = await _context.TableFoods.Where(tb => tb.Status == 0).Where(tb => !numberTable.HasValue || tb.Name == numberTable).CountAsync();

            }
             else if (stateTable == "activeTable")
            {
                dbTableFoods = await _context.TableFoods
                    .OrderBy(tb => tb.Name).Where(tb => tb.Status == 1).Where(tb =>!numberTable.HasValue || tb.Name == numberTable)
                    .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                    .ToListAsync();
                count = await _context.TableFoods.Where(tb => tb.Status == 1).Where(tb => !numberTable.HasValue || tb.Name == numberTable).CountAsync();

            }
            else
            {
                dbTableFoods = await _context.TableFoods.Where(tb => !numberTable.HasValue || tb.Name == numberTable)
                   .OrderBy(tb => tb.Name)
                   .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                   .ToListAsync();
                count = await _context.TableFoods.Where(tb => !numberTable.HasValue || tb.Name == numberTable).CountAsync();
            }
            response.Data = dbTableFoods;
            response.TotalPage = count;
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
            var dbTable = await _context.TableFoods.Where(tb => tb.Name == TableFoodDto.Name).AnyAsync();
            if(dbTable)
            {
                response.Status = false;
                response.Message = "table already exists";
                return response;
            }  
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
             
                var tableFood = new TableFood
                {
                    IdTable = Id,
                    Name = TableFoodDto.Name,
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
                    response.Message = "Không tìm thấy bàn ăn";
                    return response;
                }
                if (dbTableFood.Name == TableFoodDto.Name) {
                response.Status = false;
                response.Message = "Bàn đã tồn tại";
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
            var orders = await _context.Orders.Where(o=> o.IdTable == Id).ToListAsync();
            foreach (var order in orders)
            {
                order.IdTable = null;
            }
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
