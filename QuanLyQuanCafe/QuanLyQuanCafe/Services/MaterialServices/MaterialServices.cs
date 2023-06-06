using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Material;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace QuanLyQuanCafe.Services.MaterialServices
{
    public class MaterialServices : IMaterialService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public static int PAGE_SIZE { get; set; } = 6;
        public static int PAGE_SIZE_MATERIAL { get; set; } = 5;
        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
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

        public async Task<ApiResponse<List<Material>>> GetAllMaterial(int page, string? name)
        {
            var response = new ApiResponse<List<Material>>();
            if (string.IsNullOrEmpty(name))
            {
                var dbMaterials = await _context.Materials.OrderByDescending(m=> m.CreatedAt)
                     .Skip((page - 1) * PAGE_SIZE_MATERIAL).Take(PAGE_SIZE_MATERIAL)
                    .ToListAsync();
                response.Data = dbMaterials;
                response.TotalPage = _context.Materials.Count();
            }
            if(name != null)
            {
                var searchValue = ConvertToUnSign(name);
                var query = _context.Materials.Where(delegate (Material c)
                {
                    if (ConvertToUnSign(c.NameMaterial).IndexOf(searchValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                }).AsQueryable();
                response.TotalPage = query.ToList().Count();
                response.Data = query.OrderByDescending(m => m.CreatedAt).Skip((page - 1) * PAGE_SIZE_MATERIAL).Take(PAGE_SIZE_MATERIAL).ToList();
            }
            return response;
        }

        public async Task<ApiResponse<Material>> CreateMaterial(MaterialDto materialDto)
        {
            var response = new ApiResponse<Material>();
            string Id = Guid.NewGuid().ToString().Substring(0, 10);
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
            if (dbMaterial == null) {
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
            var dbMaterial = await _context.Materials.SingleOrDefaultAsync(m=> m.IdMaterial == Id);
           var dbUseMaterials =  _context.UseMaterials.Where(u=> u.IdMaterial == Id);
            var dbImportGood = _context.DetailImportGoods.Where(u => u.IdMaterial == Id);
            if (dbMaterial == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _context.DetailImportGoods.RemoveRange(dbImportGood);
            _context.UseMaterials.RemoveRange(dbUseMaterials);
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

        public async Task<ApiResponse<List<HistoryWarehouse>>> getHistoryWarehouse(int page, string? timeStart, string? timeEnd)
        {
            var response = new ApiResponse<List<HistoryWarehouse>>();
            var historyWarehouse = new List<HistoryWarehouse>();
            var dbMaterials =await _context.Materials.Include(m=> m.DetailImportGoods).Include(m=>m.UseMaterials)
                                                .ToListAsync();   
           foreach(var material in dbMaterials)
            {
                foreach (var detail in material.DetailImportGoods)
                {
                    DateTime date = DateTime.ParseExact(detail.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    historyWarehouse.Add(new HistoryWarehouse
                    {
                        Id = detail.IdDetailImportGoods,
                        NameMaterial = material.NameMaterial,
                        CreatedAt = date.ToString("dd/MM/yyyy HH:mm:ss"),
                        Unit = material.Unit,   
                        Amount = detail.Amount, 
                    }) ;
                }     
                foreach(var useMaterial in material.UseMaterials)
                {
                    var dbProduct = await _context.Products.Include(p=>p.OrderDetails).SingleOrDefaultAsync(p=>p.IdProduct==useMaterial.IdProduct);
                    if(dbProduct != null)
                    {
                        foreach(var item in dbProduct.OrderDetails)
                        {
                            DateTime date = DateTime.ParseExact(item.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"), "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                            historyWarehouse.Add(new HistoryWarehouse
                            {
                                Id = item.IdProduct,
                                NameMaterial = material.NameMaterial,
                                CreatedAt = date.ToString("dd/MM/yyyy HH:mm:ss"),
                                Unit = material.Unit,
                                Amount = - (float?)(item.Amout * useMaterial.Amount),
                            });
                        }
                    }
                }
                
            }
             var data = new List<HistoryWarehouse>();
            if (timeStart != null && timeEnd != null)
            {
                DateTime startDateTime = DateTime.ParseExact(timeStart, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDateTime = DateTime.ParseExact(timeEnd, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var filteredHistory =  historyWarehouse.Where(h => DateTime.ParseExact(h.CreatedAt, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) >= startDateTime &&
                                                        DateTime.ParseExact(h.CreatedAt, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) <= endDateTime)
                                                         .ToList();

                // Thực hiện phân trang dữ liệu với các phần tử thỏa mãn điều kiện đã lọc
                data = filteredHistory.OrderByDescending(h => DateTime.ParseExact(h.CreatedAt, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture))
                                         .Skip((page - 1) * PAGE_SIZE)
                                         .Take(PAGE_SIZE)
                                         .ToList();
                response.TotalPage = filteredHistory.Count();    
            }
            else
            {
                data = historyWarehouse.OrderByDescending(h => DateTime.ParseExact(h.CreatedAt, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture))
                                        .Skip((page - 1) * PAGE_SIZE)
                                        .Take(PAGE_SIZE)
                                        .ToList();
                response.TotalPage = historyWarehouse.Count();
            }
            response.Status = true; 
          
            response.Data = data;    
            
            return response;
        }
    }
}
