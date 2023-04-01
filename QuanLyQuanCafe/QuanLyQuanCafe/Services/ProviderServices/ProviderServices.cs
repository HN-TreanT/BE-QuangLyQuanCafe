using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
namespace QuanLyQuanCafe.Services.ProvideServices
{
    public class ProviderServices:IProviderService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public ProviderServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<List<Provider>>> GetAllProvider()
        {
            var response = new ApiResponse<List<Provider>>();
            try
            {
                var dbProviders = await _context.Providers.ToListAsync();
                if (dbProviders.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbProviders;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<Provider>> GetProviderById(string Id)
        {
            var response = new ApiResponse<Provider>();
            try
            {
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if (dbProvider == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
                response.Data = dbProvider;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<Provider>> CreateProvider(ProviderDto ProviderDto)
        {
            var response = new ApiResponse<Provider>();
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var provider = new Provider
                {
                    IdProvider = Id,
                    Name = ProviderDto.Name,
                    PhoneNumber = ProviderDto.PhoneNumber,
                    Address = ProviderDto.Address,
                    Email = ProviderDto.Email,

                };
                _context.Providers.Add(provider);
                await _context.SaveChangesAsync();
                response.Data = provider;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<Provider>> UpdateProvider(string Id, ProviderDto ProviderDto)
        {
            var response = new ApiResponse<Provider>();
            try
            {
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if (dbProvider == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
                _mapper.Map(ProviderDto, dbProvider);
                _context.Providers.Update(dbProvider);
                await _context.SaveChangesAsync();
                response.Data = dbProvider;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AnyType>> Deleteprovider(string Id)
        {
            var response = new ApiResponse<AnyType>();
            try
            {
                var dbProvider = await _context.Providers.SingleOrDefaultAsync(p => p.IdProvider == Id);
                if (dbProvider == null)
                {
                    response.Status= false;
                    response.Message = "not found";
                    return response;
                }
                _context.Providers.Remove(dbProvider);
                await _context.SaveChangesAsync();
 
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
