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
            try
            {
                var DtIGoods = await _context.DetailImportGoods.FindAsync(Id);
                if(DtIGoods == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = DtIGoods;   

            }catch (Exception ex) {
                response.Status = false;
                response.Message = ex.Message;  
            }
            return response;
        }

        public async Task<ApiResponse<List<DetailImportGood>>> GetAllDTGoods()
        {
            var response = new ApiResponse<List<DetailImportGood>>();
            try
            {
                var ListDtIGoods = await _context.DetailImportGoods.ToListAsync();
                if(ListDtIGoods.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = ListDtIGoods;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<DetailImportGood>> CreateDtIGoods(ImportGoodsDto DtIGoods)
        {
            var response = new ApiResponse<DetailImportGood>();
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0,10);  
                var ImportGoods = new DetailImportGood {
                    IdDetailImportGoods = Id,   
                    IdProduct = DtIGoods.IdProduct, 
                    IdProvider = DtIGoods.IdProvider,   
                    Amount = DtIGoods.Amount,   
                    Price = DtIGoods.Price, 
                };
                _context.DetailImportGoods.Add(ImportGoods);
                await _context.SaveChangesAsync();
                response.Data = ImportGoods;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<DetailImportGood>> UpdateDtIGoods(string Id, ImportGoodsDto DtIGoods)
        {
            var response = new ApiResponse<DetailImportGood>();
            try
            {
                var dbImportGoods = await _context.DetailImportGoods.FindAsync(Id);
                if(dbImportGoods == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                _mapper.Map(DtIGoods, dbImportGoods);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteDtIGoods(string Id)
        {
            var response = new ApiResponse<AnyType>();
            try
            {
                var dbImportGoods = await _context.DetailImportGoods.FindAsync(Id);
                if (dbImportGoods == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                _context.DetailImportGoods.Remove(dbImportGoods);
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
