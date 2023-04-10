using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.PProduct;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.PProductServices;
using QuanLyQuanCafe.Tools;
using System.Reflection.Metadata.Ecma335;

namespace QuanLyQuanCafe.Services.PProductServices
{
    public class PProductServices:IPProductService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public PProductServices(IMapper mapper, CafeContext context)
        {
            _mapper = mapper;
            _context = context;
        }   

        public async Task<ApiResponse<PromotionProduct>> GetPPById(string Id)
        {
            var response = new ApiResponse<PromotionProduct>();
            var dbPProduct = await _context.PromotionProducts.Include(pp=>pp.IdPromotionNavigation)
                              .Include(pp=>pp.IdProductNavigation).SingleOrDefaultAsync(pp=>pp.IdPp == Id);
            if (dbPProduct == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbPProduct;
            return response;
        }

        public async Task<ApiResponse<List<PromotionProduct>>> GetAllPP()
        {
            var response = new ApiResponse<List<PromotionProduct>>();
            var dbPProduct = await _context.PromotionProducts.Include(pp => pp.IdPromotionNavigation)
                              .Include(pp => pp.IdProductNavigation).ToListAsync();
            if (dbPProduct.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbPProduct;
            return response;
        }

        public async Task<ApiResponse<PromotionProduct>> CreatePP(PromotionProductDto ppDto)
        {
            var response = new ApiResponse<PromotionProduct>();
            var newPProduct = new PromotionProduct
            {
                IdPp = Guid.NewGuid().ToString().Substring(0,10),
                IdProduct = ppDto.IdProduct,
                IdPromotion = ppDto.IdPromotion,
                MinCount = ppDto.MinCount,
                Sale = ppDto.Sale,
            }; 
            _context.PromotionProducts.Add(newPProduct);    
            await _context.SaveChangesAsync();
            response.Data = newPProduct;
            return response;
        }

        public async Task<ApiResponse<PromotionProduct>> UpdatePP(string Id, PromotionProductDto ppDto)
        {
            var response = new ApiResponse<PromotionProduct>();
            var dbPProduct = await _context.PromotionProducts.Include(pp => pp.IdPromotionNavigation)
                              .Include(pp => pp.IdProductNavigation).SingleOrDefaultAsync(pp => pp.IdPp == Id);
            if (dbPProduct == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _mapper.Map(ppDto, dbPProduct);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeletePP(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbPProduct = await _context.PromotionProducts.FindAsync(Id);
            if (dbPProduct == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _context.PromotionProducts.Remove(dbPProduct);  
            await _context.SaveChangesAsync();  
            return response;
        }

    }
}
