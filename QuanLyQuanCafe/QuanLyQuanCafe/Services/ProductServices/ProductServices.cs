using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Controllers;
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Dto.UseMaterial;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.UseMaterialServices;
using QuanLyQuanCafe.Tools;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Linq;

namespace QuanLyQuanCafe.Services.ProductServices
{
    public class ProductOrderStatistic
    {
        public string? IdProduct { get; set; }
        public string? Title { get; set; }
        public int? TotalAmount { get; set; }
        public double? Price { get; set; }  
    }
    public class ProductServices:IProductService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        private readonly IUseMaterialServices _useMaterialServices;
        public static int PAGE_SIZE { get; set; } = 4;
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
        public ProductServices(CafeContext context , IMapper mapper,IUseMaterialServices useMaterialServices) 
        { 
           _context = context;
            _mapper = mapper;
            _useMaterialServices = useMaterialServices; 
        }
        public async Task<ApiResponse<Product>> GetProductById(string Id)
        {
            var response = new ApiResponse<Product>();
            /*  var product = await _context.Products.FindAsync(Id);*/
            var product = await _context.Products.Include(p => p.UseMaterials)
                 .ThenInclude(u => u.IdMaterialNavigation).Include(p=>p.IdCategoryNavigation)
                 .SingleOrDefaultAsync(p => p.IdProduct == Id);
                if (product == null) {
                    response.Status = false;
                    response.Message = "not found product";
                    return response;
                 }
                response.Data = product;
            return response;
        }

        public async Task<ApiResponse<List<Product>>> GetAllProduct(int pageSize, int page, string? typeSearch, string? searchValue)
        {
            var response = new ApiResponse<List<Product>>();
            IQueryable<Product> query = Enumerable.Empty<Product>().AsQueryable();
            if (string.IsNullOrEmpty(searchValue) || searchValue == null)
            {
                query = _context.Products.Include(p => p.UseMaterials).Include(p => p.IdCategoryNavigation);
                response.Data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                response.TotalPage = query.ToList().Count();
                return response;
            }
            if (typeSearch == "nameProduct")
                {
                var value = ConvertToUnSign(searchValue);
                query = _context.Products.Include(p => p.UseMaterials).Include(p => p.IdCategoryNavigation).Where(delegate (Product c)
                    {
                        if (ConvertToUnSign(c.Title).IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                }
           if (typeSearch == "category")
                {
                var value = ConvertToUnSign(searchValue);
                query = _context.Products.Include(p => p.UseMaterials).Include(p => p.IdCategoryNavigation).Where(delegate (Product c)
                    {
                        if (ConvertToUnSign(c.IdCategoryNavigation.Name).IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                }
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Data = products;
            response.TotalPage = query.ToList().Count();
        
            return response;
        }
        public async Task<ApiResponse<List<Product>>> GetAllProductByIdCategory(int pageSize, int page, string Id, string? searchValue)
        {
            var response = new ApiResponse<List<Product>>();

            IQueryable<Product> query = Enumerable.Empty<Product>().AsQueryable();
            if (string.IsNullOrEmpty(searchValue) || searchValue == null)
            {
                query = _context.Products.Where(p=>p.IdCategory == Id);
                response.Data =await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                response.TotalPage = query.ToList().Count();
                return response;
            }
            var value = ConvertToUnSign(searchValue);
            query = _context.Products.Where(delegate (Product c)
            {
                if (ConvertToUnSign(c.Title).IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
                else
                    return false;
            })
            .Where(p=> p.IdCategory == Id)
            .AsQueryable();
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Data = products;
            response.TotalPage = query.ToList().Count();
            return response;
        }
       
        public async Task<ApiResponse<Product>> CreatePoduct(ProductDto productDto)
        {
            var response = new ApiResponse<Product>();
           
            string Id = Guid.NewGuid().ToString().Substring(0,10);
            var dbProduct =await _context.Products.SingleOrDefaultAsync(p=> p.Title == productDto.Title );
            if (dbProduct != null)
            {
                response.Status = false;
                response.Message = "Mặt hàng đã tồn tại";
                return response;
            }
            var special = Guid.NewGuid().ToString();
            var newProduct = new Product
                {
                    IdProduct = Id,
                    Title = productDto.Title,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Unit  = productDto.Unit,
                    IdCategory = productDto.IdCategory,
                 
                };
            if(productDto.file != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProductImage", special + "-" + productDto.file.FileName);
                    using (FileStream ms = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.file.CopyToAsync(ms);
                    }
                    var pathImage = Path.Combine("ProductImage", special + "-" + productDto.file.FileName);
                    newProduct.Thumbnail = pathImage;
                }
            _context.Products.Add(newProduct);
             await _context.SaveChangesAsync();
             response.Data = newProduct;
            return response;
        }

        public async Task<ApiResponse<Product>> UpdatePoduct(string Id,ProductDto productDto)
        {
            var response = new ApiResponse<Product>();
                var special = Guid.NewGuid().ToString();
                var dbProduct = await _context.Products.FindAsync(Id);               
                if(dbProduct == null)
                {
                    response.Status = false;
                    response.Message = "not found product";
                    return response;
                }
                if(productDto.file != null)
                {
                    if(dbProduct.Thumbnail != null) {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", dbProduct.Thumbnail);
                        Console.WriteLine("Check path -->", path.ToString());
                        if(File.Exists(path))
                        {
                            File.Delete(path);
                        } 
                   
                    }
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ProductImage", special + "-" + productDto.file.FileName);
                    using (FileStream ms = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.file.CopyToAsync(ms);
                    }
                    var pathImage = Path.Combine("ProductImage", special + "-" + productDto.file.FileName);
                    dbProduct.Thumbnail = pathImage;
                }
                _mapper.Map(productDto,dbProduct);
                await _context.SaveChangesAsync();
                response.Data = dbProduct;           
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeletePoduct(string Id)
        {
            Console.WriteLine(Id);  
            var response = new ApiResponse<AnyType>();         
            var dbProduct = await _context.Products.Include(u => u.UseMaterials).Include(u=> u.OrderDetails).SingleOrDefaultAsync(p => p.IdProduct == Id);        
            if (dbProduct == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;    
                }

            foreach (var item in dbProduct.OrderDetails)
            {
                var dbOrderDetail = await _context.UseMaterials.FindAsync(item.IdOrderDetail);
                if (dbOrderDetail != null)
                {
                    _context.UseMaterials.Remove(dbOrderDetail);
                }
            }
            foreach (var useMaterial in dbProduct.UseMaterials)
            {
                var dbUseMaterial = await _context.UseMaterials.FindAsync(useMaterial.IdUseMaterial);
                if (dbUseMaterial != null)
                {
                    _context.UseMaterials.Remove(dbUseMaterial);
                }
            }   
            if (dbProduct.Thumbnail != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", dbProduct.Thumbnail);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

             }
            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();         
            return response;
        }
        public async Task<ApiResponse<List<ProductOrderStatistic>>> GetBestSellProduct(int time)
        {
            var response = new ApiResponse<List<ProductOrderStatistic>>();
            var top5ProductsOrdered = _context.OrderDetails
             .Where(od => od.CreatedAt >= DateTime.Now.AddDays(-time))
             .GroupBy(od => od.IdProduct)
             .Select(g => new ProductOrderStatistic
             {
                 IdProduct = g.Key,
                 Title = g.First().IdProductNavigation.Title,
                 TotalAmount = g.Sum(od => od.Amout),
                 Price = g.Sum(od => od.Price),   
              })
            .OrderByDescending(s => s.TotalAmount)
            .Take(5)    
           .ToList();
            response.Data = top5ProductsOrdered;
            return response;
        }
    }
}
