using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.UseMaterial;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.UseMaterialServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseMaterialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IUseMaterialServices _useMaterialServices;
        public UseMaterialController(IMapper mapper, CafeContext context, IUseMaterialServices useMaterialServices)
        {
            _mapper = mapper;
            _context = context;
            _useMaterialServices = useMaterialServices;
        }
        [HttpGet]
        [Route("getUseMaterialById/{Id}")]
        public async Task<IActionResult> GetUseMaterialById(string Id) {
            try
            {
                var response = await _useMaterialServices.GetUseMaterialById(Id);   
                return Ok(response);
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllUseMaterial")]
        public async Task<IActionResult> GetAllUseMaterial()
        {
            try
            {
                var response = await _useMaterialServices.GetAllUseMaterial();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createUseMaterial")]
        public async Task<IActionResult> CreateUseMaterial([FromBody] UseMaterialDto useMaterialDto)
        {
            try
            {
                var response = await _useMaterialServices.CreateUseMaterial(useMaterialDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateUseMaterial/{Id}")]
        public async Task<IActionResult> UpdateUseMaterial(string Id,[FromBody] UseMaterialDto useMaterialDto)
        {
            try
            {
                var response = await _useMaterialServices.UpdateUseMaterial(Id,useMaterialDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteUseMaterial/{Id}")]
        public async Task<IActionResult> DeleteUseMaterial(string Id)
        {
            try
            {
                var response = await _useMaterialServices.DeleteUseMaterial(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
