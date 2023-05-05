using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.ImportGoods;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using System.Globalization;
using System.Xml.Linq;

namespace QuanLyQuanCafe.Services.ImportGoodsServices
{
    public class ImportGoodsServices:IImportGoodsService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public static int PAGE_SIZE { get; set; } = 5;
        public ImportGoodsServices(CafeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DetailImportGood>> GetDTGoodsById(string Id)
        {
            var response = new ApiResponse<DetailImportGood>();
    
             var DtIGoods = await _context.DetailImportGoods.Include(dt=> dt.IdMaterialNavigation)
                                         .SingleOrDefaultAsync(dt=> dt.IdDetailImportGoods == Id);
            if (DtIGoods == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = DtIGoods;
            return response;
        }

        public async Task<ApiResponse<List<DetailImportGood>>> GetAllDTGoods(int page, string? timeStart, string? timeEnd, string? nameMaterials)
        {
            var response = new ApiResponse<List<DetailImportGood>>();        
            string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
            var ListDtIGoods = _context.DetailImportGoods
                .Include(dt => dt.IdMaterialNavigation);

            if (!string.IsNullOrEmpty(timeStart) && !string.IsNullOrEmpty(timeEnd))
            {
                var convertTimeStart = DateTime.ParseExact(timeStart, format, CultureInfo.InvariantCulture);
                var convertTimeEnd = DateTime.ParseExact(timeEnd, format, CultureInfo.InvariantCulture);
                ListDtIGoods = ListDtIGoods.Where(dt => dt.CreatedAt >= convertTimeStart && dt.CreatedAt <= convertTimeEnd)
               .Include(dt => dt.IdMaterialNavigation);

            }
            var result = await ListDtIGoods
                .OrderByDescending(dt => dt.CreatedAt)
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();
                /*var count = await _context.DetailImportGoods.Include(dt => dt.IdMaterialNavigation).CountAsync();*/
                var count = ListDtIGoods.Count();
                if (result.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
            response.Data = result;
            response.TotalPage = count;
            return response;
        }

        public async Task<ApiResponse<DetailImportGood>> CreateDtIGoods(ImportGoodsDto DtIGoods)
        {
            var response = new ApiResponse<DetailImportGood>();
            var dbMaterial = await _context.Materials.FindAsync(DtIGoods.IdMaterial);
            if (dbMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            string Id = Guid.NewGuid().ToString().Substring(0, 10);
            var ImportGoods = new DetailImportGood
            {
                IdDetailImportGoods = Id,
                NameProvider = DtIGoods.NameProvider,
                PhoneProvider = DtIGoods.PhoneProvider,
                IdMaterial = DtIGoods.IdMaterial,
                Amount = DtIGoods.Amount,
                Price = DtIGoods.Price,
            };
            var total = dbMaterial.Amount + DtIGoods?.Amount;
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
            if (dbImportGoods == null)
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

        public async Task<ApiResponse<List<DetailImportGood>>> CreateManyDtIGoods(List<ImportGoodsDto> ListDtIGoods)
        {
            var response = new ApiResponse<List<DetailImportGood>>();

            foreach (var item in ListDtIGoods)
            {
                var dbMaterial = await _context.Materials.FindAsync(item.IdMaterial);
                if (dbMaterial == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var ImportGoods = new DetailImportGood
                {
                    IdDetailImportGoods = Id,
                    NameProvider = item.NameProvider,
                    PhoneProvider = item.PhoneProvider,
                    IdMaterial = item.IdMaterial,
                    Amount = item.Amount,
                    Price = item.Price,
                };
                var total = dbMaterial.Amount + item?.Amount;
                dbMaterial.Amount = total;
                _context.DetailImportGoods.Add(ImportGoods);
            }
            await _context.SaveChangesAsync();

            return response;
        }

    }
}
