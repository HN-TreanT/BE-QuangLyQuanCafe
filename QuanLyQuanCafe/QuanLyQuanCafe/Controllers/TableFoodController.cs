using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe.Controllers
{
    public class InfoTableFood
    {
        public string? Name { get; set; }
        public byte? Status { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TableFoodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;

        public TableFoodController(IMapper mapper, CafeContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        [HttpGet]
        [Route("getAllTableFood")]
        public async Task<IActionResult> GetAllTableFood()
        {
            try
            {
                var dbTableFoods = await _context.TableFoods.ToListAsync();
                if(dbTableFoods.Count <= 0 )
                {
                    return NotFound();
                }
               return  Ok(new ApiResponse<List<TableFood>> { Status = true, Message = "Success",Data = dbTableFoods });
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getTableFoodDetail/{Id}")]
        public async Task<IActionResult> GetAllTableFood(string Id)
        {
            try
            {
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if(dbTableFood == null)
                {
                    return NotFound();
                }
                return Ok(new ApiResponse<TableFood> { Status = true, Message = "Success",Data = dbTableFood });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createTableFood")]
        public async Task<IActionResult> CreateTableFood([FromBody] InfoTableFood InfotableFood )
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0,10);
                var tableFood = new TableFood
                {
                    IdTable = Id,
                    Name = InfotableFood.Name,
                    Status = InfotableFood.Status,
                };
                _context.TableFoods.Add(tableFood);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<TableFood> { Status = true, Message = "Success",Data = tableFood });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateTablefood/{Id}")]
        public async Task<IActionResult> UpdateTableFood(string Id, [FromBody] InfoTableFood tableFood)
        {
            try
            {
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if (dbTableFood == null)
                {
                    return NotFound();
                }
                _mapper.Map(tableFood, dbTableFood);
                _context.TableFoods.Update(dbTableFood);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<TableFood> { Status = true, Message = "Success",Data = dbTableFood });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteTableFood/{Id}")]
        public async Task<IActionResult> DeleteTableFood(string Id)
        {
            try
            {
                var dbTableFood = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == Id);
                if (dbTableFood == null)
                {
                    return NotFound();
                }
                _context.TableFoods.Remove(dbTableFood);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
