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
            var dbProdcut = await _context.Products.Include(p=>p.PromotionProducts).SingleOrDefaultAsync(p=>p.IdProduct==orderDetailDto.IdProduct);
            if (dbProdcut == null) {
                response.Status =false;
                response.Message = "Not found product";
                return response;
            }
            var newPrice = orderDetailDto.Amount * dbProdcut.Price;
            foreach (var item in dbProdcut.PromotionProducts)
            {
                if(item.MinCount <= orderDetailDto.Amount)
                {
                    newPrice -= newPrice * item.Sale;
                }
            }
            orderDetailDto.Price = newPrice;    
            var newOrderDetail = new OrderDetail
            {
                IdOrderDetail = Guid.NewGuid().ToString().Substring(0, 10),
                IdOrder = orderDetailDto.IdOrder,
                IdProduct = orderDetailDto.IdProduct,
                Price = orderDetailDto.Price ,   
                Amout = orderDetailDto.Amount
            };
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

            dbOrderDetail.Amout = updateOrderDetailDto.Amount;
            dbOrderDetail.Price = updateOrderDetailDto.Amount * dbProduct.Price;
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
                var dbProdcut = await _context.Products.Include(p => p.PromotionProducts).SingleOrDefaultAsync(p => p.IdProduct == item.IdProduct);
                if (dbProdcut == null)
                {
                    response.Status = false;
                    response.Message = "Not found product";
                    return response;
                }
                var newPrice = item.Amount * dbProdcut.Price; 
                foreach (var item1 in dbProdcut.PromotionProducts)
                {
                    Console.WriteLine($"item1.MinCount: {item1.MinCount}, item.Amount: {item.Amount}");
                    if (item1.MinCount <= item.Amount)
                    {
                        newPrice -= newPrice * item1.Sale;
                    }
                }    
                item.Price = newPrice;

                var newOrderDetail = new OrderDetail
                {
                    IdOrderDetail = Guid.NewGuid().ToString().Substring(0, 10),
                    IdOrder = item.IdOrder,
                    IdProduct = item.IdProduct,
                    Price =item.Price,
                    Amout = item.Amount
                };
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
            var ActualSaleMoney = _context.OrderDetails.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                       .Sum(od => od.Price);
            //tiền hàng = tổng giá trị Mặt hàng* số lượng
            var MoneyProduct = _context.OrderDetails.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                    .Sum(Od => Od.IdProductNavigation != null ? Od.IdProductNavigation.Price * Od.Amout : 0);
            //tiền nhập :
            var MoneyMaterial = _context.DetailImportGoods.Where(c => c.CreatedAt >= DateTime.Now.AddDays(-time))
                                    .Sum(d=>d.Price);    
            var sale = MoneyProduct - ActualSaleMoney;
            //doanh thu:
          ///  var Revenue = ActualSaleMoney - MoneyMaterial;
            var newOverview = new Overview
            {
                CustomerNumber= CustomerCount,
                OrderNumber = OrdersCount,
                ProductNumber = ProductCount,   
                Revenue = ActualSaleMoney,
                MoneyProduct = MoneyProduct,
                Sale = sale ,
                MoneyMaterial = MoneyMaterial   
            };
            response.Data = newOverview;
            return response;
        }

        

    }
}
