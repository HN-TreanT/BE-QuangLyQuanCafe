using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe.Controllers
{

    public class InfoCustomer
    {
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public CustomerController(CafeContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }
        [HttpGet]
        [Route("getAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {var dbCustomers = await _context.Customers.ToListAsync();
                if(dbCustomers.Count <= 0)
                {
                    return Ok(new ApiResponse<AnyType> { Status = false, Message = "not found customer" });
                }
                return Ok(new ApiResponse<List<Customer>> { Status = true, Message = "sucess",Data = dbCustomers });

            }catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status=false, Message=ex.Message });
            }
        }
                
        [HttpGet]
        [Route("getCutomerById/{Id}")]
        public async Task<IActionResult> GetCustomerById(string Id)
        {
            try
            {
                var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.IdCustomer == Id);
                if(dbCustomer == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status= false, Message = "Not found customer"});
                }
                return Ok(new ApiResponse<Customer> { Status = true, Message = "sucess",Data = dbCustomer });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] InfoCustomer infoCustomer)
        {
            try
            {
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var customer = new Customer
                {
                    IdCustomer = Id,
                    Fullname = infoCustomer.Fullname,
                    Gender = infoCustomer.Gender,
                    PhoneNumber = infoCustomer.PhoneNumber
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<Customer> { Status = true, Message = "sucess", Data = customer });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("updateInfoCustomer/{Id}")]
        public async Task<IActionResult> UpdateInfoCustomer(string Id, [FromBody] InfoCustomer infoUpdate)
        {
            try
            {
                var dbCustomer =await _context.Customers.SingleOrDefaultAsync(c => c.IdCustomer == Id);
                if(dbCustomer == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = true, Message = "Not found customer" });
                }
                _mapper.Map(infoUpdate, dbCustomer);
                _context.Customers.Update(dbCustomer);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<Customer> { Status = true, Message = "sucess",Data = dbCustomer });

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
                var dbCustomer = await _context.Customers.SingleOrDefaultAsync(u => u.IdCustomer == Id);
                if (dbCustomer == null)
                {
                    return Ok(new ApiResponse<AnyType> { Status = true, Message = "Not found customer" });
                }
                _context.Customers.Remove(dbCustomer);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<AnyType> { Status = true, Message = "sucess" });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }

    }
}
