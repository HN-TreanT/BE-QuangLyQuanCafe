using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Customer;

namespace QuanLyQuanCafe.Services.CustomerServices
{
    public interface ICustomerServices
    {
        Task<ApiResponse<List<Customer>>> GetAllCustomer(int page,string? name);
        Task<ApiResponse<Customer>> GetCustomerById(string Id);
        Task<ApiResponse<Customer>> CreateCustomer(CustomerDto CustomerDto);

        Task<ApiResponse<Customer>> UpdateCustomerDto(string Id, CustomerDto CustomerDto);
        Task<ApiResponse<AnyType>> DeleteCustomer(string Id);
        Task<ApiResponse<List<Customer>>> SearchCustomerByName(int page,string CustomerName);
        Task<ApiResponse<Customer>> SearchCustomerByPhone(string CustomerPhone);
    }
}
