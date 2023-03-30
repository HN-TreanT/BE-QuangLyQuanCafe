using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using AutoMapper;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShiftController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public WorkShiftController(CafeContext _context, IMapper mapper)
        {
            this._context = _context;
            this._mapper = mapper;
        }

        public class InfoWS
        {
            public int? WorkShift1 { get; set; }
            public TimeSpan? ArrivalTime { get; set; }
            public TimeSpan? TimeOn { get; set; }

        }
        [HttpGet]
        [Route("GetWorkShift")]
        public async Task<IActionResult> GetWorkShift()
        {
            try
            {
                var dbWorkShifts = await _context.WorkShifts.ToListAsync();   
               return Ok(new ApiResponse<List<WorkShift>> { Status=true, Message= "Success", Data = dbWorkShifts });

            }catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status=false, Message= ex.Message });
            }
        }
        [HttpPost]
        [Route("CreateWorkShift")]
        public async Task<IActionResult> CreateWorshift(InfoWS workShift)
        {
            try
            {
                var newWorkShift = new WorkShift
                {
                    WorkShift1 = workShift.WorkShift1,
                    ArrivalTime = workShift.ArrivalTime,
                    TimeOn = workShift.TimeOn
                };
                _context.WorkShifts.Add(newWorkShift);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<List<WorkShift>> { Status = true, Message = "Success"});
            }
            catch(Exception ex) {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateWorkShift/{Id}")]
        public async Task<IActionResult> UpdateWorkShift(int Id, [FromBody] InfoWS workShift )
        {
            try
            {
                var DbWorkShift = await _context.WorkShifts.SingleOrDefaultAsync(u => u.IdWorkShift == Id);
                if(DbWorkShift == null)
                {
                    return NotFound(new ApiResponse<AnyType> { Status = false, Message = "Not found " });
                }
                 _mapper.Map(workShift,DbWorkShift);
                _context.WorkShifts.Update(DbWorkShift);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<WorkShift> { Status = true, Message = "Success" ,Data= DbWorkShift});
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteWS/{Id}")]
        public async Task<IActionResult> DeleteWorkShift(int Id)
        {
            try
            {
                var dbWorkShift = await _context.WorkShifts.FindAsync(Id);
                if(dbWorkShift == null)
                {
                    return NotFound(new ApiResponse<AnyType> { Status = true, Message = "Not found" });
                }
                _context.WorkShifts.Remove(dbWorkShift);
                await _context.SaveChangesAsync();
                return Ok(new ApiResponse<List<WorkShift>> { Status = true, Message = " delete Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
