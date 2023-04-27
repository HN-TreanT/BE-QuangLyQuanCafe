using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Material;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
namespace QuanLyQuanCafe.Services.MaterialServices
{
    public class MaterialServices:IMaterialService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        
        public MaterialServices(IMapper mapper, CafeContext context)
        {
            _mapper = mapper;
            _context = context;
        }   

        public async Task<ApiResponse<Material>> GetMaterialById(string Id)
        {
            var response = new ApiResponse<Material>();
            var dbMaterial = await _context.Materials.FindAsync(Id);
            if (dbMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found Material";
                return response;
            }
            response.Data = dbMaterial; 
            return response;
        }

        public async Task<ApiResponse<List<Material>>> GetAllMaterial()
        {
            var response = new ApiResponse<List<Material>>();
            var dbMaterials = await _context.Materials.ToListAsync();
            if(dbMaterials.Count <= 0) {
                response.Status = false;
                response.Message = "Not found material";
                return response;
            }
            response.Data = dbMaterials;
            return response;
        }

        public async Task<ApiResponse<Material>> CreateMaterial(MaterialDto materialDto)
        {
            var response = new ApiResponse<Material>();
            string Id = Guid.NewGuid().ToString().Substring(0,10);
            var material = new Material
            {
                IdMaterial = Id,
                NameMaterial = materialDto.NameMaterial,
                Description = materialDto.Description,
                Unit = materialDto.Unit,
                Expiry = materialDto.Expiry,    
            };
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            response.Data = material;
            return response;
        }

        public async Task<ApiResponse<Material>> UpdateMaterial(string Id, MaterialUpdateDto materialUpdateDto)
        {
            var response = new ApiResponse<Material>();
            var dbMaterial = await _context.Materials.FindAsync(Id);
            if(dbMaterial == null) {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _mapper.Map(materialUpdateDto, dbMaterial); 
            await _context.SaveChangesAsync();
            response.Data = dbMaterial;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteMaterial(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbMaterial = await _context.Materials.FindAsync(Id);
            if (dbMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _context.Materials.Remove(dbMaterial);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ApiResponse<List<Material>>> searchMaterialByName(string Name)
        {
            var response = new ApiResponse<List<Material>>();
            var dbMaterials = _context.Materials.AsEnumerable()
                                           .Where(m => _Convert.ConvertToUnSign(m.NameMaterial).Contains(_Convert.ConvertToUnSign(Name)))
                                           .ToList();
            response.Data = dbMaterials;
            return response;
        }
    }
}
