using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Customer;

namespace QuanLyQuanCafe.Services.CustomerServices
{
    public class CustomerServices:ICustomerServices
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public CustomerServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ApiResponse<List<Customer>>> GetAllCustomer()
        {
            var response = new ApiResponse<List<Customer>>();
                var dbCustomers = await _context.Customers.ToListAsync();
                if (dbCustomers.Count <= 0)
                {
                    response.Message = "Not found";
                    return response;
                 }
                response.Data = dbCustomers;          
            return response;
        }

        public async Task<ApiResponse<Customer>> GetCustomerById(string Id)
        {
            var response = new ApiResponse<Customer>();        
                var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.IdCustomer == Id);
                if (dbCustomer == null)
                {
                    response.Message = "not found customer";
                    return response;
                }
                response.Data = dbCustomer;         
            return response;
        }

        public async Task<ApiResponse<Customer>> CreateCustomer(CustomerDto CustomerDto)
        {
            var response = new ApiResponse<Customer>();          
                string Id = Guid.NewGuid().ToString().Substring(0, 10);
                var customer = new Customer
                {
                    IdCustomer = Id,
                    Fullname = CustomerDto.Fullname,
                    Gender = CustomerDto.Gender,
                    PhoneNumber = CustomerDto.PhoneNumber
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                response.Data = customer;                
            return response;
        }

        public async Task<ApiResponse<Customer>> UpdateCustomerDto(string Id, CustomerDto CustomerDto)
        {
            var response = new ApiResponse<Customer>();           
                /* var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.IdCustomer == Id);*/
                var dbCustomer = await _context.Customers.FindAsync(Id);
                if (dbCustomer == null)
                {
                    response.Status = false;
                    response.Message = "Not found customer";
                    return response;
                }
                _mapper.Map(CustomerDto, dbCustomer);
                _context.Customers.Update(dbCustomer);
                await _context.SaveChangesAsync();         
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteCustomer(string Id)
        {
            var response = new ApiResponse<AnyType>();          
                var dbCustomer = await _context.Customers.SingleOrDefaultAsync(u => u.IdCustomer == Id);
                if (dbCustomer == null)
                {
                    response.Message = "Not found customer";
                    return response;
                }
                _context.Customers.Remove(dbCustomer);
                await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ApiResponse<List<Customer>>>  SearchCustomerByName(string CustomerName)
        {
            var response = new ApiResponse<List<Customer>>();                    
                    var dbCustomers = await _context.Customers
                   .Where(c => c.Fullname != null && c.Fullname.Contains(CustomerName))
                   .ToListAsync();
                   response.Data = dbCustomers;
          
            return response;
        }

        public async Task<ApiResponse<Customer>> SearchCustomerByPhone(string CustomerPhone)
        {
            var response = new ApiResponse<Customer>();         
                var dbCustomer =await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber != null && c.PhoneNumber.Equals(CustomerPhone));
                if (dbCustomer == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbCustomer;                          
            return response;

        }
    }
}
