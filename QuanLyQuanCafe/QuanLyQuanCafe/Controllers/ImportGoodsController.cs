using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetImportGoodDt(string Id)
        {
            try
            {
                var response = await _importGoodsService.GetDTGoodsById(Id);
                return Ok(response);
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllImportGood")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllImportGood(int page,string? timeStart,string? timeEnd,string? nameMaterials)
        {
            try
            {
                var response = await _importGoodsService.GetAllDTGoods(page,timeStart,timeEnd,nameMaterials);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createImportGoods")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateImportGoods([FromBody] ImportGoodsDto ImportGoodsDto)
        {
            try
            {
                var response = await _importGoodsService.CreateDtIGoods(ImportGoodsDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateImportGoods/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateImportGoods(string Id, [FromBody] ImportGoodsDto ImportGoodsDto)
        {
            try
            {
                var response = await _importGoodsService.UpdateDtIGoods(Id,ImportGoodsDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteImportGoods/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteImportGoods(string Id)
        {
            try
            {
                var response = await _importGoodsService.DeleteDtIGoods(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("createManyImportGoods")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateManyImportGoods([FromBody] List<ImportGoodsDto> listImportGoods)
        {
            try
            {
                var response = await _importGoodsService.CreateManyDtIGoods(listImportGoods);
                return Ok(response);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
             }
        }
    }
}
