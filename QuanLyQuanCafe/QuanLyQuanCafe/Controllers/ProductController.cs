using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.ProductServices;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(CafeContext context, IMapper mapper, IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        [Route("getProductById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string Id) {
            try
            {
                var response = await _productService.GetProductById(Id);
                return Ok(response);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpGet]
        [Route("getAllProduct")]
        [Authorize]
        public async Task<IActionResult> GetAllProduct(int pageSize,int page , string? typeSearch,string? searchValue)
        {
            try
            {
                var response = await _productService.GetAllProduct(pageSize,page,typeSearch,searchValue);   
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllProductByCategory")]
        [Authorize]
        public async Task<IActionResult> GetAllProductByCategory(int pageSize,int page,string Id,string? searchValue)
        {
            try
            {
                var response = await _productService.GetAllProductByIdCategory(pageSize,page,Id,searchValue);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("createProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _productService.CreatePoduct(productDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("updateProduct/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(string Id,[FromForm]ProductDto productDto)
        {
            try
            {
                var response = await _productService.UpdatePoduct(Id,productDto);    
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteProduct/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            try
            {
                var response = await _productService.DeletePoduct(Id);    
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getBestSellProduct/{time}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBestSellProduct(int time)
        {
            try
            {
                var response = await _productService.GetBestSellProduct(time);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
