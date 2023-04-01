using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.ProvideServices;
namespace QuanLyQuanCafe.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IProviderService _serviceProvider;
        public ProviderController(CafeContext context, IMapper mapper,IProviderService providerService)
        {
            this._mapper = mapper;
            this._context = context;
            this._serviceProvider = providerService;
        }
        [HttpGet]
        [Route("getAllProvider")]
        public async Task<IActionResult> GetAllProvider()
        {
            try
            {
               var response = await _serviceProvider.GetAllProvider();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getProviderById/{Id}")]
        public async Task<IActionResult> GetProviderById(string Id)
        {
            try
            {
              var response = await _serviceProvider.GetProviderById(Id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createProvider")]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderDto ProviderDto)
        {
            try
            {

                var response = await _serviceProvider.CreateProvider(ProviderDto);
                return Ok(response);    
               

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateProvider/{Id}")]
        public async Task<IActionResult> UpdateProvider(string Id, [FromBody] ProviderDto ProviderDto)
        {
            try
            {
                var response = await _serviceProvider.UpdateProvider(Id,ProviderDto);
                return Ok(response);
     
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }


        [HttpDelete]
        [Route("deleteProvider/{Id}")]
        public async Task<IActionResult> Deleteprovider( string Id)
        {
            try
            {
                var response = await _serviceProvider.Deleteprovider(Id);
                return Ok(response);
                

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
