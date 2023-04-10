using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Promotion;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.PromotionServices
{
    public class PromotionServices:IPromotionService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public PromotionServices(IMapper mapper, CafeContext context)
        {
            _mapper = mapper;
            _context = context;
        }   

        public async Task<ApiResponse<Promotion>> GetPromotionById(string Id)
        {
            var response = new ApiResponse<Promotion>();
            var dbPromotion = await _context.Promotions.FindAsync(Id);
            if(dbPromotion == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;    
            }
            response.Data = dbPromotion;
            return response;
        }

        public async Task<ApiResponse<List<Promotion>>> GetAllPromotion()
        {
            var response = new ApiResponse<List<Promotion>>();
            var dbPromotions = await _context.Promotions.ToListAsync();
            if(dbPromotions.Count <= 0)
            {
                response.Status = false;
                response.Message = "not found";
                return response;
            }
            response.Data = dbPromotions;
            return response;
        }


        public async Task<ApiResponse<Promotion>> CreatePromotion(PromotionDto promotionDto)
        {
            var response = new ApiResponse<Promotion>();
            var newPromotion = new Promotion
            {
                IdPromotion = Guid.NewGuid().ToString().Substring(0, 10),
                Name = promotionDto.Name,
                TimeStart = promotionDto.TimeStart,
                TimeEnd = promotionDto.TimeEnd,
                Status = 1
            };
            _context.Promotions.Add(newPromotion);
            await _context.SaveChangesAsync();  
            response.Data = newPromotion;   
            return response;
        }


        public async Task<ApiResponse<Promotion>> UpdatePromotion(string Id, PromotionDto promotionDto)
        {
            var response = new ApiResponse<Promotion>();
            var dbPromotion = await _context.Promotions.FindAsync(Id);
            if (dbPromotion == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _mapper.Map(promotionDto, dbPromotion);
            await _context.SaveChangesAsync();
            response.Data = dbPromotion;
            return response;
        }



        public async Task<ApiResponse<AnyType>> DeletePromotion(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbPromotion = await _context.Promotions.Include(p => p.PromotionProducts).SingleOrDefaultAsync(p=>p.IdPromotion==Id);

            if (dbPromotion == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            foreach(var item in dbPromotion.PromotionProducts)
            {
                _context.PromotionProducts.Remove(item);
            }
            _context.Promotions.Remove(dbPromotion);
            await _context.SaveChangesAsync();  
            return response;
        }



    }
}
