using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe.Controllers
{
    public class ProviderInfo
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Email { get; set; } = null!;
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public ProviderController(CafeContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }
        [HttpGet]
        [Route("getAllProvider")]
        public async Task<IActionResult> GetAllProvider()
        {
            try
            {
                var dbProviders = await _context.Providers.ToListAsync();
                if(dbProviders.Count <= 0)
                {
                    return Ok(new ApiResponse<AnyType> { Status = true, Message = "Not found provider" });
                }
                return Ok(new ApiResponse<List<Provider>> { Status = false, Message = "success",Data = dbProviders });

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
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if(dbProvider == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "not found usser" });
                }
                return Ok(new ApiResponse<Provider> { Status = true, Message = "success",Data = dbProvider });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createProvider")]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderInfo providerInfo)
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var provider = new Provider
                {
                    IdProvider = Id,
                    Name = providerInfo.Name,
                    PhoneNumber = providerInfo.PhoneNumber,
                    Address = providerInfo.Address,
                    Email = providerInfo.Email,

                };
                _context.Providers.Add(provider);
                await _context.SaveChangesAsync();  
                return Ok(new ApiResponse<Provider> { Status = false, Message = "success", Data = provider });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateProvider/{Id}")]
        public async Task<IActionResult> UpdateProvider(string Id, [FromBody] ProviderInfo providerInfo)
        {
            try
            {
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if(dbProvider == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "Not found provider" });
                }
                _mapper.Map(providerInfo, dbProvider);
                _context.Providers.Update(dbProvider);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<Provider> { Status = false, Message = "success", Data = dbProvider });

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
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if(dbProvider == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = " not found provider" });
                }
                _context.Providers.Remove(dbProvider);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<AnyType> { Status = false, Message = "success" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
