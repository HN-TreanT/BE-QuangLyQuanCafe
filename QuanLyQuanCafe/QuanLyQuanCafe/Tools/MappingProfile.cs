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
using QuanLyQuanCafe.Dto.Product;
using QuanLyQuanCafe.Dto.ImportGoods;
using QuanLyQuanCafe.Dto.Material;
using QuanLyQuanCafe.Dto.UseMaterial;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Dto.OrderDetail;


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
            CreateMap<ProductDto, Product>()
               .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
              .ForMember(dest => dest.Thumbnail, opt => opt.Condition(src => src.Thumbnail != null))
              .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
              .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != null))
              .ForMember(dest => dest.Unit, opt => opt.Condition(src => src.Unit != null))
              .ForMember(dest => dest.IdCategory, opt => opt.Condition(src => src.IdCategory != null));
            CreateMap<ImportGoodsDto, DetailImportGood>()
               .ForMember(dest => dest.IdMaterial, opt => opt.Condition(src => src.IdMaterial != null))
               .ForMember(dest => dest.NameProvider, opt => opt.Condition(src => src.NameProvider != null))
               .ForMember(dest => dest.PhoneProvider, opt => opt.Condition(src => src.PhoneProvider != null))
               .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null))
               .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != null));
            CreateMap<MaterialDto, Material>()
               .ForMember(dest => dest.NameMaterial, opt => opt.Condition(src => src.NameMaterial != null))
               .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
               .ForMember(dest => dest.Unit, opt => opt.Condition(src => src.Unit != null))
               .ForMember(dest => dest.Expiry, opt => opt.Condition(src => src.Expiry != null))
               .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null));
            CreateMap<MaterialUpdateDto, Material>()
                 .ForMember(dest => dest.NameMaterial, opt => opt.Condition(src => src.NameMaterial != null))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
               .ForMember(dest => dest.Unit, opt => opt.Condition(src => src.Unit != null))
               .ForMember(dest => dest.Expiry, opt => opt.Condition(src => src.Expiry != null));
            CreateMap<UseMaterialDto,UseMaterial>()
               .ForMember(dest => dest.IdMaterial, opt => opt.Condition(src => src.IdMaterial != null))
               .ForMember(dest => dest.IdProduct, opt => opt.Condition(src => src.IdProduct != null))
               .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null));
            CreateMap<OrderDto,Order>()
               .ForMember(dest => dest.IdCustomer, opt => opt.Condition(src => src.IdCustomer != null))
               .ForMember(dest => dest.IdTable, opt => opt.Condition(src => src.IdTable != null))
               .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null))
               .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null));
            CreateMap<UpdateOrderDetail, OrderDetail>()
               .ForMember(dest => dest.Amout, opt => opt.Condition(src => src.Amount != null));
           
               



        }
    }
}
