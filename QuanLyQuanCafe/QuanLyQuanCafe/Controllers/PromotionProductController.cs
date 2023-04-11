using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.PProduct;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.PProductServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IPProductService _pproductService;
        public PromotionProductController(IMapper mapper, CafeContext context, IPProductService pproductService)
        {
            _mapper = mapper;
            _context = context;
            _pproductService = pproductService;
        }
        [HttpGet]
        [Route("getPPById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetPPById(string Id)
        {
            try
            {
                var response = await _pproductService.GetPPById(Id);
                return Ok(response);

            }catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("getAllPP")]
        [Authorize]
        public async Task<IActionResult> GetAllPP()
        {
            try
            {
                var response = await _pproductService.GetAllPP();
                return Ok(response);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        [Route("createPP")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePP([FromBody] PromotionProductDto ppDto)
        {
            try
            {
                var response = await _pproductService.CreatePP(ppDto);
                return Ok(response);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Route("updatePP/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePP(string Id, [FromBody] PromotionProductDto ppDto)
        {
            try
            {
                var response = await _pproductService.UpdatePP(Id,ppDto);
                return Ok(response);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete]
        [Route("deletePP/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePP(string Id)
        {
            try
            {
                var response = await _pproductService.DeletePP(Id);
                return Ok(response);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
