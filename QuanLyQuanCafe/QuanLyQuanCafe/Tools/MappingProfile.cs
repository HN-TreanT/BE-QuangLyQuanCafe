using AutoMapper;
using QuanLyQuanCafe.Models;
using static QuanLyQuanCafe.Controllers.WorkShiftController;

namespace QuanLyQuanCafe.Tools
{
    public class MappingProfile :Profile
    {
       public MappingProfile()
        {
          /*  CreateMap<WorkShift, InfoWS>();*/
            CreateMap<InfoWS, WorkShift>()
                 .ForMember(dest => dest.WorkShift1, opt => opt.Condition(src => src.WorkShift1 != null))
                 .ForMember(dest => dest.ArrivalTime, opt => opt.Condition(src => src.ArrivalTime != null))
                 .ForMember(dest => dest.TimeOn, opt => opt.Condition(src => src.TimeOn != null));
        }
    }
}
