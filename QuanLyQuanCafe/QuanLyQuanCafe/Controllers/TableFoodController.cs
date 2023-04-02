using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.TableFoodServices;
using QuanLyQuanCafe.Dto.TableFood;

namespace QuanLyQuanCafe.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TableFoodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly ITableFoodService _tableFoodService;

        public TableFoodController(IMapper mapper, CafeContext context,ITableFoodService tableFoodService)
        {
            this._mapper = mapper;
            this._context = context;
            this._tableFoodService = tableFoodService;  
        }

        [HttpGet]
        [Route("getAllTableFood")]
        public async Task<IActionResult> GetAllTableFood()
        {
            try
            {
                var response = await _tableFoodService.GetAllTableFood();   
                return Ok(response);
                
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
                var response = await _tableFoodService.GetTableFoodById(Id);
                return Ok(response);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createTableFood")]
        public async Task<IActionResult> CreateTableFood([FromBody] TableFoodDto TableFoodDto )
        {
            try
            {
                var response = await _tableFoodService.CreateTableFood(TableFoodDto);
                return Ok(response);
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateTablefood/{Id}")]
        public async Task<IActionResult> UpdateTableFood(string Id, [FromBody] TableFoodDto tableFood)
        {
            try
            {
                var response = await _tableFoodService.UpdateTableFood(Id,tableFood);
                return Ok(response);
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
                var response = await _tableFoodService.DeleteTableFood(Id);
               return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
