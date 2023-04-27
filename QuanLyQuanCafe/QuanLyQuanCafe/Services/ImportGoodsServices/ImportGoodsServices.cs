using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.ImportGoods;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.ImportGoodsServices
{
    public class ImportGoodsServices:IImportGoodsService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public ImportGoodsServices(CafeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DetailImportGood>> GetDTGoodsById(string Id)
        {
            var response = new ApiResponse<DetailImportGood>();
          
            var DtIGoods = await _context.DetailImportGoods.FindAsync(Id);
            if(DtIGoods == null) {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            var dbProduct = await _context.Products.FindAsync(DtIGoods.IdMaterial);
            var dbProvider = await _context.Providers.FindAsync(DtIGoods.IdProvider);
            if(dbProduct == null && dbProvider == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
           // DtIGoods.IdMaterialNavigation = dbProduct;
            DtIGoods.IdProviderNavigation = dbProvider;
            response.Data = DtIGoods;   
            return response;
        }

        public async Task<ApiResponse<List<DetailImportGood>>> GetAllDTGoods()
        {
            var response = new ApiResponse<List<DetailImportGood>>();
            var ListDtIGoods = await _context.DetailImportGoods
              .Include(d => d.IdMaterialNavigation)
              .Include(d => d.IdProviderNavigation)
              .Select(d => new DetailImportGood
              {
                  IdDetailImportGoods = d.IdDetailImportGoods,
                  IdMaterial = d.IdMaterial,
                  IdProvider = d.IdProvider,
                  Amount = d.Amount,
                  Price = d.Price,  
                  IdMaterialNavigation = d.IdMaterialNavigation,
                  IdProviderNavigation = d.IdProviderNavigation,

              }).ToListAsync();
            if (ListDtIGoods.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
             response.Data = ListDtIGoods;


            return response;
        }

        public async Task<ApiResponse<DetailImportGood>> CreateDtIGoods(ImportGoodsDto DtIGoods)
        {
            var response = new ApiResponse<DetailImportGood>();
            var dbMaterial  = await _context.Materials.FindAsync(DtIGoods.IdMaterial);
            if(dbMaterial == null) {
              response.Status=false;
                response.Message = "Not found";
                return response;
            }
            string Id = Guid.NewGuid().ToString().Substring(0,10);
            var ImportGoods = new DetailImportGood {
                IdDetailImportGoods = Id,
                IdProvider = DtIGoods.IdProvider,
                IdMaterial = DtIGoods.IdMaterial,
                Amount = DtIGoods.Amount,
                Price = DtIGoods.Price,            
            };
            var total  = dbMaterial.Amount + DtIGoods?.Amount;
            dbMaterial.Amount = total;
            _context.DetailImportGoods.Add(ImportGoods);
            await _context.SaveChangesAsync();
            response.Data = ImportGoods;
            return response;
        }

        public async Task<ApiResponse<DetailImportGood>> UpdateDtIGoods(string Id, ImportGoodsDto DtIGoods)
        {
            var response = new ApiResponse<DetailImportGood>();
                var dbImportGoods = await _context.DetailImportGoods.FindAsync(Id);
                if(dbImportGoods == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                _mapper.Map(DtIGoods, dbImportGoods);
                await _context.SaveChangesAsync();
            response.Data = dbImportGoods;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteDtIGoods(string Id)
        {
            var response = new ApiResponse<AnyType>();
           
                var dbImportGoods = await _context.DetailImportGoods.FindAsync(Id);
                if (dbImportGoods == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                _context.DetailImportGoods.Remove(dbImportGoods);
                await _context.SaveChangesAsync();  
           
            return response;
        }

    }
}
