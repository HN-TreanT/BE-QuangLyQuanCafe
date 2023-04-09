﻿using AutoMapper;
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
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var response = await _orderService.GetAllOrder();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("creatOrder")]
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
    }
}
