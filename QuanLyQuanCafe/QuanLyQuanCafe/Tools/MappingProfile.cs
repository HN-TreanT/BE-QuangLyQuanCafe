using AutoMapper;
using QuanLyQuanCafe.Controllers;
using QuanLyQuanCafe.Models;
using static QuanLyQuanCafe.Controllers.WorkShiftController;
using QuanLyQuanCafe.Dto.Category;
using QuanLyQuanCafe.Dto.Customer;
using QuanLyQuanCafe.Dto.Provider;
using QuanLyQuanCafe.Dto.TableFood;
using QuanLyQuanCafe.Dto.WorkShift;
using QuanLyQuanCafe.Dto.Staff;

namespace QuanLyQuanCafe.Tools
{
    public class MappingProfile :Profile
    {
       public MappingProfile()
        {
            CreateMap<WorkShiftDto, WorkShift>()
                 .ForMember(dest => dest.IdWorkShift, opt => opt.Condition(src => src.IdWorkShift != null))
                 .ForMember(dest => dest.ArrivalTime, opt => opt.Condition(src => src.ArrivalTime != null))
                 .ForMember(dest => dest.TimeOn, opt => opt.Condition(src => src.TimeOn != null));
            CreateMap<UpdateWorkShiftDto, WorkShift>()
                 .ForMember(dest => dest.ArrivalTime, opt => opt.Condition(src => src.ArrivalTime != null))
                 .ForMember(dest => dest.TimeOn, opt => opt.Condition(src => src.TimeOn != null));
            CreateMap<CustomerDto,Customer>()
                .ForMember(dest => dest.Fullname, opt => opt.Condition(src => src.Fullname != null))
                .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
                .ForMember(dest => dest.Gender, opt => opt.Condition(src => src.Gender != null));
            CreateMap<ProviderDto, Provider>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
                .ForMember(dest => dest.Address, opt => opt.Condition(src => src.Address != null))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null));
            CreateMap<CategoryDto, Category>()
               .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null));
            CreateMap<TableFoodDto, TableFood>()
              .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
              .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));
            CreateMap<StaffCreateDto, staff>()
              .ForMember(dest => dest.Fullname, opt => opt.Condition(src => src.Fullname != null))
              .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
              .ForMember(dest => dest.Gender, opt => opt.Condition(src => src.Gender != null))
              .ForMember(dest => dest.Birthday, opt => opt.Condition(src => src.Birthday != null))
              .ForMember(dest => dest.Address, opt => opt.Condition(src => src.Address != null))
              .ForMember(dest => dest.Salary, opt => opt.Condition(src => src.Salary != null))
              .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null));
        }
    }
}
