using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.OrderDetail;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.OrderDetailServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IOrderDtService _orderDtService;
        public OrderDetailsController(IMapper mapper, CafeContext context, IOrderDtService orderDtService)
        {
            _mapper = mapper;
            _context = context;
            _orderDtService = orderDtService;
        }
        [HttpGet]
        [Route("getOrderDtById/{Id}")]
        
        public async Task<IActionResult> GetOrderDtById(string Id)
        {
            try
            {
                var response = await _orderDtService.GetOrderDtById(Id);
                return Ok(response);
            }catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllOrderDt")]

        public async Task<IActionResult> GetAllOrderDt()
        {
            try
            {
                var response = await _orderDtService.GetAllOrderDt();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllOrderByIdOrder/{IdOrder}")]

        public async Task<IActionResult> GetAllOrderByIdOrder(string IdOrder)
        {
            try
            {
                var response = await _orderDtService.GetAllOrderDtByIdOrder(IdOrder);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createOrderDetail")]

        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailDto orderDetailDto)
        {
            try
            {
                var response = await _orderDtService.CreateOrderDt(orderDetailDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateOrderDt/{Id}")]

        public async Task<IActionResult> UpdateOrderDt(string Id,[FromBody] UpdateOrderDetail updateorderDetailDto)
        {
            try
            {
           
                var response = await _orderDtService.UpdateOrderDt(Id, updateorderDetailDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteOrderDt/{Id}")]

        public async Task<IActionResult> DeleteOrderDt(string Id)
        {
            try
            {
                var response = await _orderDtService.DeleteOrderDt(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createListOrderDt")]
        public async Task<IActionResult> CreateListOrderDt([FromBody] List<OrderDetailDto> listOrderDt)
        {
            try
            {
                var response = await _orderDtService.CreateListOrderDt(listOrderDt);
                return Ok(response);
            }catch (Exception ex) { 
                        
                return
                    BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOverview/{time}")]
        public async Task<IActionResult> GetOverview(int time)
        {
            try
            {
                var response = await _orderDtService.GetOverview(time);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return
                    BadRequest(ex.Message);
            }
        }
    }
}
