using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.CustomerServices;
using QuanLyQuanCafe.Dto.Customer;

namespace QuanLyQuanCafe.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly ICustomerServices _customerServices;
        public CustomerController(CafeContext context, IMapper mapper, ICustomerServices customerServices)
        {
            this._mapper = mapper;
            this._context = context;
            this._customerServices = customerServices;
        }
        [HttpGet]
        [Route("getAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var response = await _customerServices.GetAllCustomer();
                return Ok(response);

            } catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getCutomerById/{Id}")]
        public async Task<IActionResult> GetCustomerById(string Id)
        {
            try
            {
                var response = await _customerServices.GetCustomerById(Id);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto CustomerDto)
        {
            try
            {
                var response = await _customerServices.CreateCustomer(CustomerDto);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateCustomerDto/{Id}")]
        public async Task<IActionResult> UpdateCustomerDto(string Id, [FromBody] CustomerDto infoUpdate)
        {
            try
            {
                var response = await _customerServices.UpdateCustomerDto(Id, infoUpdate);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }


        [HttpDelete]
        [Route("deleteCustomer/{Id}")]
        public async Task<IActionResult> DeleteCustomer(string Id)
        {
            try
            {
                var response = await _customerServices.DeleteCustomer(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("searchByName/{CustomerName}")]
        public async Task<IActionResult> SearchByName( string CustomerName)
        {
            try
            {
              var response = await _customerServices.SearchCustomerByName(CustomerName);
              return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("searchByPhone/{CustomerPhone}")]
        public async Task<IActionResult> SearchByPhone(string CustomerPhone)
        {
            try
            {
                var response = await _customerServices.SearchCustomerByPhone(CustomerPhone);    
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
