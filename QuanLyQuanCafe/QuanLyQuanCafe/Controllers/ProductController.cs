using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.ProductServices;

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
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var response = await _productService.GetAllProduct();   
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("createProduct")]
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


    }
}
