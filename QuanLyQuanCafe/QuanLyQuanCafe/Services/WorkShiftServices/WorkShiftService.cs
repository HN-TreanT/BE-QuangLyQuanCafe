using AutoMapper;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.WorkShift;

namespace QuanLyQuanCafe.Services.WorkShiftServices
{
    public class WorkShiftService:IWorkShiftService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;
        public WorkShiftService(CafeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<ApiResponse<WorkShift>> GetWorkShiftDetail(int Id)
        {
            var response = new ApiResponse<WorkShift>();
            var dbWorkShifts = await _context.WorkShifts.Include(ws => ws.SelectedWorkShifts).ThenInclude(sws=> sws.IdStaffNavigation ).SingleOrDefaultAsync(ws=> ws.IdWorkShift == Id);
            if (dbWorkShifts == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbWorkShifts;
            return response;
        }

        public async Task<ApiResponse<List<WorkShift>>> GetWorkShift()
        {
            var response = new ApiResponse<List<WorkShift>>();
              //  var dbWorkShifts = await _context.WorkShifts.ToListAsync();
               var dbWorkShifts = await _context.WorkShifts.Include(ws=> ws.SelectedWorkShifts).ToListAsync();
                if(dbWorkShifts == null)
                {
                    response.Status = false;
                    response.Message = "Not found";
                    return response;
                }
                response.Data = dbWorkShifts;          
            return response;
        }

      

        public async Task<ApiResponse<WorkShift>> CreateWorkShift(WorkShiftDto WorkShiftDto)
        {
            var response = new ApiResponse<WorkShift>();
            var dbWorkShift = await _context.WorkShifts.FindAsync(WorkShiftDto.IdWorkShift);
            if(dbWorkShift != null)
            {
                response.Status = false;
                response.Message = "work shift exist";
                return response;
            } 
               var newWorkShift = new WorkShift
                {
                    IdWorkShift = WorkShiftDto.IdWorkShift,
                    ArrivalTime = WorkShiftDto.ArrivalTime,
                    TimeOn = WorkShiftDto.TimeOn
                };
                _context.WorkShifts.Add(newWorkShift);
                await _context.SaveChangesAsync();
                response.Data = newWorkShift;        
            return response;
        }

        public async Task<ApiResponse<WorkShift>> UpdateWorkShift(int Id, UpdateWorkShiftDto WorkShiftDto)
        {
            var response = new ApiResponse<WorkShift>();
                var DbWorkShift = await _context.WorkShifts.SingleOrDefaultAsync(u => u.IdWorkShift == Id);
                if (DbWorkShift == null)
                {
                    response.Status = false;
                    response.Message = "Email  bt";
                   return response;
                }
                _mapper.Map(WorkShiftDto, DbWorkShift);
                _context.WorkShifts.Update(DbWorkShift);
                await _context.SaveChangesAsync();
               response.Data = DbWorkShift;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteWorkShift(int Id)
        {
            var response = new ApiResponse<AnyType>();
                var dbWorkShift = await _context.WorkShifts.FindAsync(Id);
                if (dbWorkShift == null)
                {
                    response.Status = false;
                    response.Message = "not found";
                    return response;
                }
                var ListSelectedWS = _context.SelectedWorkShifts
                                  .Where(sws => sws.IdWorkShift == dbWorkShift.IdWorkShift);
                _context.SelectedWorkShifts.RemoveRange(ListSelectedWS);
               _context.WorkShifts.Remove(dbWorkShift);
                await _context.SaveChangesAsync();
            return response;
        }
    }
}
