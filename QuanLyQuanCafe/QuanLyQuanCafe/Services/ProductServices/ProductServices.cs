using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Controllers;
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.ProductServices
{
    public class ProductServices:IProductService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public ProductServices(CafeContext context , IMapper mapper) 
        { 
           _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<Product>> GetProductById(string Id)
        {
            var response = new ApiResponse<Product>();

            try
            {
                var product = await _context.Products.FindAsync(Id);
                if (product == null) {
                    response.Status = false;
                    response.Message = "not found product";
                    return response;
                 }
                response.Data = product;

            }catch (Exception ex) {
                response.Status = false;
                response.Message = ex.Message;
                return response;
           }
            return response;
        }

        public async Task<ApiResponse<List<Product>>> GetAllProduct()
        {
            var response = new ApiResponse<List<Product>>();

            try
            {
                var products = await _context.Products.ToListAsync();
                if(products.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found product";
                    return response;
                }
                response.Data = products;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ApiResponse<Product>> CreatePoduct(ProductDto productDto)
        {
            var response = new ApiResponse<Product>();

            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0,10);
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

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ApiResponse<Product>> UpdatePoduct(string Id,ProductDto productDto)
        {
            var response = new ApiResponse<Product>();

            try
            {
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
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeletePoduct(string Id)
        {
            var response = new ApiResponse<AnyType>();
            try
            {
                var dbProduct = await _context.Products.FindAsync(Id);  
                if (dbProduct == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;    
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

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}
