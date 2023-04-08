using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.UseMaterial;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.UseMaterialServices
{
    public class UseMaterialServices:IUseMaterialServices
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public UseMaterialServices(IMapper mapper, CafeContext context)
        {
            _mapper = mapper;   
            _context = context;
        }
        public async Task<ApiResponse<UseMaterial>> GetUseMaterialById(string Id)
        {
            var response = new ApiResponse<UseMaterial>();
            var dbUseMaterial = await _context.UseMaterials.FindAsync(Id);
            if (dbUseMaterial == null) {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbUseMaterial;
            return response;
        }

        public async Task<ApiResponse<List<UseMaterial>>> GetAllUseMaterial()
        {
            var response = new ApiResponse<List<UseMaterial>>();
            var dbUseMaterial = await _context.UseMaterials.ToListAsync();
            if(dbUseMaterial.Count <= 0) {
                response.Status = false;
                response.Message = "Not found";
                return response;    
            }
            response.Data = dbUseMaterial;  
            return response;
        }

        public async Task<ApiResponse<UseMaterial>> CreateUseMaterial(UseMaterialDto useMaterialDto)
        {
            var response = new ApiResponse<UseMaterial>();
            string Id = Guid.NewGuid().ToString().Substring(0,10);
            var useMaterial = new UseMaterial
            {
                IdUseMaterial = Id,
                IdProduct = useMaterialDto.IdProduct,   
                IdMaterial = useMaterialDto.IdMaterial, 
                Amount = useMaterialDto.Amount,
            };
            _context.UseMaterials.Add(useMaterial); 
            await _context.SaveChangesAsync();
            response.Data = useMaterial;
            return response;
        }

        public async Task<ApiResponse<UseMaterial>> UpdateUseMaterial(string Id, UseMaterialDto useMaterialDto)
        {
            var response = new ApiResponse<UseMaterial>();
            var dbUseMaterial = await _context.UseMaterials.FindAsync(Id);
            if (dbUseMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _mapper.Map(useMaterialDto, dbUseMaterial);
            await _context.SaveChangesAsync();
            response.Data = dbUseMaterial;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteUseMaterial(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbUseMaterial = await _context.UseMaterials.FindAsync(Id);
            if (dbUseMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _context.UseMaterials.Remove(dbUseMaterial);    
            await _context.SaveChangesAsync();  
            return response;
        }
    }
}
