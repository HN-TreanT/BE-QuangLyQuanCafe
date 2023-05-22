using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Services.OrderServices;

namespace QuanLyQuanCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly IOrderService _orderService;
        public OrderController(IMapper mapper, CafeContext context, IOrderService orderService)
        {
            _mapper = mapper;
            _context = context;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("getOrderById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(string Id)
        {
            try
            {
                var response = await _orderService.GetOrderById(Id);    
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);          
            }
        }

        [HttpGet]
        [Route("getAllOrder")]
        [Authorize]
        public async Task<IActionResult> GetAllOrder(int page,string? typeSearch, string? searchValue)
        {
            try
            {
                var response = await _orderService.GetAllOrder(page,typeSearch,searchValue);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("creatOrder")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                var response = await _orderService.CreateOrder(orderDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateOrder/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(string Id,[FromBody] OrderDto orderDto)
        {
            try
            {
                var response = await _orderService.UpdateOrder(Id,orderDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteOrder/{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(string Id)
        {
            try
            {
                var response = await _orderService.DeleteOrder(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOrderPaid")]
        [Authorize]
        public async Task<IActionResult> GetOrderPaid(int page, string? typeSearch, string? searchValue)
        {
            try
            {
                var response = await _orderService.GetOrderPaid(page,typeSearch,searchValue);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("getOrderUnpaid")]
        [Authorize]
        public async Task<IActionResult> GetOrderUnpaid(int page, string? typeSearch, string? searchValue,string? timeStart,string? timeEnd)
        {
            try
            {
                var response = await _orderService.GetOrderUnpaid(page, typeSearch, searchValue,timeStart,timeEnd);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("graftOrder")]
        [Authorize]
        public async Task<IActionResult> GraftOrder(string IdOldOrder,string IdNewOrder)
        {
            try
            {
                var response = await _orderService.GraftOrder(IdOldOrder,IdNewOrder);
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("splitOrder")]
        [Authorize]
        public async Task<IActionResult> SplitOrder(string IdOldOrder,string IdNewOrder, [FromBody] List<DataSplitOrder>? SplitOrders)
        {
            try
            {
                var response = await _orderService.SplitOrder(IdOldOrder, IdNewOrder, SplitOrders);
                return Ok(response);
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

    }
}
