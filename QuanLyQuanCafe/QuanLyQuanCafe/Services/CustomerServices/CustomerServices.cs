using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Customer;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace QuanLyQuanCafe.Services.CustomerServices
{
    public class CustomerServices:ICustomerServices
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;

        public static int PAGE_SIZE { get; set; } = 5;
        public CustomerServices(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        

        public async Task<ApiResponse<List<Customer>>> GetAllCustomer(int page, string? name)
        {
            var response = new ApiResponse<List<Customer>>();
            if (!string.IsNullOrEmpty(name)  )
            {

                    var dbCustomers =  _context.Customers.Include(cus => cus.Orders).ThenInclude(order => order.OrderDetails)
                                        .AsEnumerable()
                                       .Where(m => _Convert.ConvertToUnSign(m.Fullname).Contains(_Convert.ConvertToUnSign(name)))
                                       .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                       .ToList();
                var count = _context.Customers
                                       .AsEnumerable()
                                       .Where(m => _Convert.ConvertToUnSign(m.Fullname).Contains(_Convert.ConvertToUnSign(name)))
                                       .ToList().Count();
                if (dbCustomers.Count <= 0)
                {
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbCustomers;
                response.TotalPage = count;

                return response;
            }
            else
            {
                var dbCustomers = await _context.Customers.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).Include(cus => cus.Orders).ThenInclude(order => order.OrderDetails).ToListAsync();
                int count = await _context.Customers.CountAsync();
                if (dbCustomers.Count <= 0)
                {
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbCustomers;
                response.TotalPage = count;

                return response;
            }
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
            var dbCustomer = await _context.Customers
                                   .Where(cus=> cus.PhoneNumber == CustomerDto.PhoneNumber)
                                   .SingleOrDefaultAsync();
            Console.WriteLine(CustomerDto.PhoneNumber);
            if(CustomerDto.PhoneNumber  != null)
            {
                if(CustomerDto.PhoneNumber.Length < 10 || CustomerDto.PhoneNumber.Length >11)
                {
                    response.Status = false;
                    response.Message = "Số điện thoại không hợp lệ!";
                    return response;
                }

            }
            if (dbCustomer == null)
            {
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
            else
            {
                response.Status = false;
                response.Message = "customer already exist";
                return response;
            }
               
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
            if (CustomerDto.PhoneNumber != null)
            {
                if (CustomerDto.PhoneNumber.Length < 10 || CustomerDto.PhoneNumber.Length > 11)
                {
                    response.Status = false;
                    response.Message = "Số điện thoại không hợp lệ!";
                    return response;
                }

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

        public async Task<ApiResponse<List<Customer>>>  SearchCustomerByName(int page,   string CustomerName)
        {
            var response = new ApiResponse<List<Customer>>();
            var dbCustomers = _context.Customers
                                   .Include(cus => cus.Orders).ThenInclude(order => order.OrderDetails)
                                   .AsEnumerable()
                                   .Where(m => _Convert.ConvertToUnSign(m.Fullname).Contains(_Convert.ConvertToUnSign(CustomerName)))
                                   .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                   .ToList();
            var count = _context.Customers
                                   .AsEnumerable()
                                   .Where(m => _Convert.ConvertToUnSign(m.Fullname).Contains(_Convert.ConvertToUnSign(CustomerName)))
                                   .ToList().Count();
            response.Data = dbCustomers;
            response.TotalPage = count;
          
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
