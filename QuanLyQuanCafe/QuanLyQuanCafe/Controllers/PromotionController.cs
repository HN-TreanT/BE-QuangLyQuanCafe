using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.Promotion;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.PromotionServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IPromotionService _promotionService;
        public PromotionController(IMapper mapper, CafeContext context, IPromotionService promotionService)
        {
            _mapper = mapper;
            _context = context;
            _promotionService = promotionService;
        }

        [HttpGet]
        [Route("getPromotionById/{Id}")]
        public async Task<IActionResult> GetPromotionById(string Id) 
        {
            try
            {
                var response = await _promotionService.GetPromotionById(Id);
                return Ok(response);    
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("getAllPromotion")]
        public async Task<IActionResult> GetAllPromotion()
        {
            try
            {
                var response = await _promotionService.GetAllPromotion();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createPromotion")]
        public async Task<IActionResult> CreatePromotion([FromBody]PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.CreatePromotion(promotionDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updatePromotion/{Id}")]
        public async Task<IActionResult> UpdatePromotion(string Id, [FromBody] PromotionDto promotionDto)
        {
            try
            {
                var response = await _promotionService.UpdatePromotion(Id, promotionDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletePromotion/{Id}")]
        public async Task<IActionResult> DeletePromotion(string Id)
        {
            try
            {
                var response = await _promotionService.DeletePromotion(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
