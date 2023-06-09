using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto;
using QuanLyQuanCafe.Dto.OrderDetail;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;

namespace QuanLyQuanCafe.Services.OrderDetailServices
{
    public class OrderDtServices:IOrderDtService
    {
        private readonly CafeContext _context;
        private readonly IMapper _mapper;   
        public OrderDtServices(CafeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<OrderDetail>> GetOrderDtById(string Id)
        {
            var response = new ApiResponse<OrderDetail>();
            var dbOrderDetail = await _context.OrderDetails.Include(odt => odt.IdProductNavigation)
                                                           .SingleOrDefaultAsync(odt => odt.IdOrderDetail==Id);
            if (dbOrderDetail == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
                   
            }
            response.Data = dbOrderDetail;  

            return response;
        }

        public async Task<ApiResponse<List<OrderDetail>>> GetAllOrderDt()
        {
            var response = new ApiResponse<List<OrderDetail>>();
            var dbOrderDetails = await  _context.OrderDetails.Include(odt => odt.IdProductNavigation)
                                                           .ToListAsync();
            if(dbOrderDetails.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbOrderDetails;
            return response;
        }

        public async Task<ApiResponse<OrderDetail>> CreateOrderDt(OrderDetailDto orderDetailDto)
        {
            var response = new ApiResponse<OrderDetail>();
            var dbProdcut = await _context.Products.Include(p=>p.UseMaterials).SingleOrDefaultAsync(p=>p.IdProduct==orderDetailDto.IdProduct);
            var dbOrder = await _context.Orders.SingleOrDefaultAsync(p=> p.IdOrder == orderDetailDto.IdOrder);
            if (dbProdcut == null || dbOrder == null) {
                response.Status =false;
                response.Message = "Not found ";
                return response;    
            }
            // trừ đi nguyên liệu trong kho
            foreach (var item in dbProdcut.UseMaterials)
            {
                var materail = await _context.Materials.FindAsync(item.IdMaterial);
                if(materail != null)
                {
                    var total = materail.Amount - item.Amount * orderDetailDto.Amount;
                    materail.Amount = total;    
                }
            }
            var newOrderDetail = new OrderDetail
            {
                IdOrderDetail = Guid.NewGuid().ToString().Substring(0, 10),
                IdOrder = orderDetailDto.IdOrder,
                IdProduct = orderDetailDto.IdProduct,
                Price = dbProdcut.Price * orderDetailDto.Amount,   
                Amout = orderDetailDto.Amount
            };
            
            if(dbOrder.Price != null)
            {
                var total = dbOrder.Price + newOrderDetail.Price;
                dbOrder.Price = (long?)total;
            }
            _context.OrderDetails.Add(newOrderDetail);
            await _context.SaveChangesAsync();  
            response.Data = newOrderDetail; 
            return response;
        }

        public async Task<ApiResponse<OrderDetail>> UpdateOrderDt(string Id, UpdateOrderDetail updateOrderDetailDto)
        {
            var response = new ApiResponse<OrderDetail>();
            var dbOrderDetail = await _context.OrderDetails.FindAsync(Id);
            
            
            if (dbOrderDetail == null )
            {
                response.Status = false;
                response.Message = "Not found order detail";
                return response;

            }
            var dbProduct = await _context.Products.FindAsync(dbOrderDetail.IdProduct);
            if (dbProduct == null)
            {
                response.Status = false;
                response.Message = "Not found product";
                return response;
            }
            var dbOrder = await _context.Orders.FindAsync(dbOrderDetail.IdOrder);
            if (dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found product";
                return response;
            }
            var priceOrder = dbOrder.Price - dbOrderDetail.Price + updateOrderDetailDto.Amount * dbProduct.Price;
            dbOrderDetail.Amout = updateOrderDetailDto.Amount;
            dbOrderDetail.Price = updateOrderDetailDto.Amount * dbProduct.Price;
            dbOrder.Price = (long?)priceOrder;
            _context.OrderDetails.Update(dbOrderDetail);    
            await _context.SaveChangesAsync();
            response.Data = dbOrderDetail;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteOrderDt(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbOrderDetail = await _context.OrderDetails.FindAsync(Id);
            if (dbOrderDetail == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;

            }
            var dbOrder = await _context.Orders.FindAsync(dbOrderDetail.IdOrder);
            if(dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            var price = dbOrder.Price - dbOrderDetail.Price;
            dbOrder.Price =(long?)price;
            _context.OrderDetails.Remove(dbOrderDetail);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ApiResponse<List<OrderDetail>>> GetAllOrderDtByIdOrder(string IdOrder)
        {
            var response = new ApiResponse<List<OrderDetail>>();
            var dbOrderDetails = await _context.OrderDetails.Include(odt => odt.IdProductNavigation)
                                        .Where(odt=>odt.IdOrder == IdOrder).ToListAsync(); 
            if(dbOrderDetails.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            response.Data = dbOrderDetails;    
            return response;
        }

        public async Task<ApiResponse<List<OrderDetail>>> CreateListOrderDt(List<OrderDetailDto> ListOrderDt)
        {
            var response = new ApiResponse<List<OrderDetail>> ();
            foreach(var item in ListOrderDt)
            {
                var dbProdcut = await _context.Products.SingleOrDefaultAsync(p => p.IdProduct == item.IdProduct);
                var dbOrder = await _context.Orders.SingleOrDefaultAsync(p => p.IdOrder == item.IdOrder);
                if (dbProdcut == null || dbOrder == null)
                {
                    response.Status = false;
                    response.Message = "Not found product";
                    return response;
                }
                foreach (var item2 in dbProdcut.UseMaterials)
                {
                    var materail = await _context.Materials.FindAsync(item2.IdMaterial);
                    if (materail != null)
                    {
                        var total = materail.Amount - item.Amount * item.Amount;
                        materail.Amount = total;
                    }
                }
                var newOrderDetail = new OrderDetail
                {
                    IdOrderDetail = Guid.NewGuid().ToString().Substring(0, 10),
                    IdOrder = item.IdOrder,
                    IdProduct = item.IdProduct,
                    Price = dbProdcut.Price * item.Amount,
                    Amout = item.Amount
                };
                if (dbOrder.Price != null)
                {
                    var total = dbOrder.Price + newOrderDetail.Price;
                    dbOrder.Price = (long?)total;
                }
                _context.OrderDetails.Add(newOrderDetail);
            }           
            await _context.SaveChangesAsync();
            return response;
        }

        //Lấy ra tiền hàng , giảm giá , số khách hàng , số hóa đơn,số mặt hàng 
        public async Task<ApiResponse<Overview>> GetOverview(int time)
        {
            var response = new ApiResponse<Overview>();
            var CustomerCount = _context.Customers.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time)).Count();
            var OrdersCount = _context.Orders.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time)).Count();
            var ProductCount = _context.OrderDetails.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time)).Count();
            
            //tiền bán thực tế:(doanh thu)
            var ActualSaleMoney = _context.Orders.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                       .Sum(od => od.Price);
            //tiền hàng = tổng giá trị Mặt hàng* số lượng
            var MoneyProduct = _context.OrderDetails.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                    .Sum(Od => Od.IdProductNavigation != null ? Od.IdProductNavigation.Price * Od.Amout : 0);
            //tiền nhập :
            var MoneyMaterial = _context.DetailImportGoods.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                    .Sum(d=>d.Price);    
            //doanh thu:
          ///  var Revenue = ActualSaleMoney - MoneyMaterial;
            var newOverview = new Overview
            {
                CustomerNumber= CustomerCount,
                OrderNumber = OrdersCount,
                ProductNumber = ProductCount,   
                Revenue = ActualSaleMoney,
                MoneyProduct = ActualSaleMoney,
                MoneyMaterial = MoneyMaterial   
            };
            response.Data = newOverview;
            return response;
        }

        public async Task<ApiResponse<RevenueOverview>> RevenueOverview()
        {
            var response = new ApiResponse<RevenueOverview>();
            int currentYear = DateTime.Now.Year;
            int prevouisYear = DateTime.Now.Year - 1;
            List<decimal> monthlyCurrentYear = new List<decimal>();
            List<decimal> monthlyRevenuePreviousYear = new List<decimal>();
            for (int month = 1; month <= 12; month++)
            {
                // Lấy ngày đầu tiên và cuối cùng của tháng trong năm trước
                DateTime startDate = new DateTime(currentYear, month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                // Tính tổng doanh thu trong khoảng thời gian của tháng
                var revenueOfMonth =await _context.Orders.Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
                .SumAsync(od => od.Price);

                // Thêm tổng doanh thu vào danh sách
                monthlyCurrentYear.Add((decimal)revenueOfMonth);
            }
            for (int month = 1; month <= 12; month++)
            {
                // Lấy ngày đầu tiên và cuối cùng của tháng trong năm trước
                DateTime startDatePrevouis = new DateTime(prevouisYear, month, 1);
                DateTime endDatePrevouis = startDatePrevouis.AddMonths(1).AddDays(-1);

                // Tính tổng doanh thu trong khoảng thời gian của tháng
                var revenueOfMonthPre =await _context.Orders.Where(c => c.CreatedAt >= startDatePrevouis && c.CreatedAt <= endDatePrevouis)
                .SumAsync(od => od.Price);

                // Thêm tổng doanh thu vào danh sách
                monthlyRevenuePreviousYear.Add((decimal)revenueOfMonthPre);
            }
            response.Data = new RevenueOverview();
            response.Data.currentYear = monthlyCurrentYear;
           response.Data.preYear = monthlyRevenuePreviousYear;
            return response;
        }

    }
}
