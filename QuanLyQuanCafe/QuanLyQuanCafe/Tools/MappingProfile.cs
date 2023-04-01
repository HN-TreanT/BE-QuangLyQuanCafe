using AutoMapper;
using QuanLyQuanCafe.Controllers;
using QuanLyQuanCafe.Models;
using static QuanLyQuanCafe.Controllers.WorkShiftController;
using QuanLyQuanCafe.Dto;
namespace QuanLyQuanCafe.Tools
{
    public class MappingProfile :Profile
    {
       public MappingProfile()
        {
            CreateMap<WorkShiftDto, WorkShift>()
                 .ForMember(dest => dest.WorkShift1, opt => opt.Condition(src => src.WorkShift1 != null))
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
        }
    }
}
