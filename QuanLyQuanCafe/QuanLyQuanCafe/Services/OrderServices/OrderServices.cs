﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.OrderServices
{
    public class OrderServices:IOrderService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        public OrderServices(IMapper mapper, CafeContext context)
        {
            _mapper = mapper;
            _context = context;
        }   
        public async Task<ApiResponse<Order>> GetOrderById(string Id)
        {
            var response = new ApiResponse<Order>();
            var dbOrder = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                .Include(o => o.IdTableNavigation).SingleOrDefaultAsync(o => o.IdOrder == Id);
            if (dbOrder == null) {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            response.Data = dbOrder;
            return response;
        }

        public async Task<ApiResponse<List<Order>>> GetAllOrder()
        {
            var response = new ApiResponse<List<Order>>();
            var dbOrder = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                .Include(o => o.IdTableNavigation).ToListAsync();
            if(dbOrder.Count <= 0) {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            response.Data = dbOrder;
            return response;
        }

        public async Task<ApiResponse<Order>> CreateOrder(OrderDto orderDto)
        {
            var response = new ApiResponse<Order>();

            var newOrder = new Order
            {
                IdOrder = Guid.NewGuid().ToString().Substring(0, 10),
                IdCustomer = orderDto.IdCustomer,
                IdTable = orderDto.IdTable, 
                OrderDate = orderDto.OrderDate, 
            };
            _context.Orders.Add(newOrder);  
            await _context.SaveChangesAsync();  
            response.Data = newOrder;   
            return response;
        }

        public async Task<ApiResponse<Order>> UpdateOrder(string Id, OrderDto orderDto)
        {
            var response = new ApiResponse<Order>();
            var dbOrder = await _context.Orders.Include(o => o.IdCustomerNavigation)
                            .Include(o => o.IdTableNavigation).SingleOrDefaultAsync(o => o.IdOrder == Id);
            if(dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            _mapper.Map(orderDto, dbOrder);
            await _context.SaveChangesAsync();
            response.Data = dbOrder;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteOrder(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbOrder = await _context.Orders.FindAsync(Id);
            if(dbOrder == null) {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();  
            return response;
        }
    }
}
