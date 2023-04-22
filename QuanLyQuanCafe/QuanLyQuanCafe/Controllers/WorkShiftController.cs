using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Models;
using AutoMapper;
using QuanLyQuanCafe.Tools;
using QuanLyQuanCafe.Services.WorkShiftServices;
using QuanLyQuanCafe.Dto.WorkShift;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShiftController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IWorkShiftService _workShiftService;
        public WorkShiftController(CafeContext _context, IMapper mapper, IWorkShiftService workShiftService)
        {
            this._context = _context;
            this._mapper = mapper;
            this._workShiftService = workShiftService;
        }
        [HttpGet]
        [Route("GetWorkShiftDetail/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWorkShiftDetail(int Id)
        {
            try
            {
                var reponse =await _workShiftService.GetWorkShiftDetail(Id);
                return Ok(reponse);

            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetWorkShift")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWorkShift()
        {
            try
            {
                var response = await _workShiftService.GetWorkShift();
                return Ok(response);
               

            }catch(Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status=false, Message= ex.Message });
            }
        }
        [HttpPost]
        [Route("CreateWorkShift")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateWorkShift(WorkShiftDto workShift)
        {
            try
            {
                var response = await _workShiftService.CreateWorkShift(workShift);
                return Ok(response);
                
            }
            catch(Exception ex) {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateWorkShift/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateWorkShift(int Id, [FromBody] UpdateWorkShiftDto workShift )
        {
            try
            {
                var response = await _workShiftService.UpdateWorkShift(Id, workShift);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteWS/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWorkShift(int Id)
        {
            try
            {
                var response = await _workShiftService.DeleteWorkShift(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AnyType> { Status = false, Message = ex.Message });
            }
        }
    }
}
