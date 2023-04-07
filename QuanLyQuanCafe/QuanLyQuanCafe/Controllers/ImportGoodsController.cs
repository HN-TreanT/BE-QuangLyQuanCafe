using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.ImportGoods;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.ImportGoodsServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportGoodsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IImportGoodsService _importGoodsService;

        public ImportGoodsController(IMapper mapper, CafeContext context, IImportGoodsService importGoodsService)
        {
            _mapper = mapper;
            _context = context;
            _importGoodsService = importGoodsService;
        }

        [HttpGet]
        [Route("getImportGoodDt/{Id}")]
        public async Task<IActionResult> GetImportGoodDt(string Id)
        {
            try
            {
                var response = await _importGoodsService.GetDTGoodsById(Id);
                return Ok();
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllImportGood")]
        public async Task<IActionResult> GetAllImportGood()
        {
            try
            {
                var response = await _importGoodsService.GetAllDTGoods();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("createImportGoods")]
        public async Task<IActionResult> CreateImportGoods([FromBody] ImportGoodsDto ImportGoodsDto)
        {
            try
            {
                var response = await _importGoodsService.CreateDtIGoods(ImportGoodsDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("updateImportGoods/{Id}")]
        public async Task<IActionResult> UpdateImportGoods(string Id, [FromBody] ImportGoodsDto ImportGoodsDto)
        {
            try
            {
                var response = await _importGoodsService.UpdateDtIGoods(Id,ImportGoodsDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("deleteImportGoods/{Id}")]
        public async Task<IActionResult> DeleteImportGoods(string Id)
        {
            try
            {
                var response = await _importGoodsService.DeleteDtIGoods(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
