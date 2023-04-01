using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Models;
using Microsoft.OpenApi.Any;

namespace QuanLyQuanCafe.Services.CustomerServices
{
    public interface ICustomerServices
    {
        Task<ApiResponse<List<Customer>>> GetAllCustomer();
        Task<ApiResponse<Customer>> GetCustomerById(string Id);
        Task<ApiResponse<Customer>> CreateCustomer(CustomerDto CustomerDto);

        Task<ApiResponse<Customer>> UpdateCustomerDto(string Id, CustomerDto CustomerDto);
        Task<ApiResponse<AnyType>> DeleteCustomer(string Id);
    }
}
