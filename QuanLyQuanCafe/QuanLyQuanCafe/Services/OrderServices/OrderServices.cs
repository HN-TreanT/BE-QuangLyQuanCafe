using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using QuanLyQuanCafe.Dto.Order;
using QuanLyQuanCafe.Models;
using QuanLyQuanCafe.Tools;
using System.Text;
using System.Globalization;
using FuzzySharp;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Text.RegularExpressions;
using System.Xml;
using QuanLyQuanCafe.Dto.TokenAuth;
using System.Reflection;
using QuanLyQuanCafe.Services.TableFoodServices;
using QuanLyQuanCafe.Dto.OrderDetail;

namespace QuanLyQuanCafe.Services.OrderServices
{

    public class OrderServices : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly CafeContext _context;
        private readonly ITableFoodService _tableFoodService;
        public static int PAGE_SIZE { get; set; } = 6;
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            //text = text.Replace(" ", "-"); //Comment lại để không đưa khoảng trắng thành ký tự -

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public OrderServices(IMapper mapper, CafeContext context, ITableFoodService tableFoodService)
        {
            _mapper = mapper;
            _context = context;
            _tableFoodService = tableFoodService;
        }
        public async Task<ApiResponse<Order>> GetOrderById(string Id)
        {
            var response = new ApiResponse<Order>();
            var dbOrder = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                .Include(o => o.IdTableNavigation).SingleOrDefaultAsync(o => o.IdOrder == Id);
            if (dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            response.Data = dbOrder;
            return response;
        }

        public async Task<ApiResponse<List<OrderGet>>> GetAllOrder(int page, string? typeSearch, string? searchValue)
        {
            var response = new ApiResponse<List<OrderGet>>();
            if (typeSearch == "nameCustomer")
            {
                var data = new List<OrderGet>();
                var dbOrders1 = await _context.Orders.Include((o) => o.IdCustomerNavigation)
                                                 .Include(o => o.IdTableNavigation).OrderByDescending(p => p.CreatedAt)
                                                 .ToListAsync();
                foreach (var dbOrder in dbOrders1)
                {
                    OrderGet order = new OrderGet()
                    {
                        IdOrder = dbOrder.IdOrder.ToString(),
                        Status = dbOrder?.Status,
                        Amount = dbOrder?.Amount,
                        TimePay = dbOrder?.TimePay,
                        CreatedAt = dbOrder?.CreatedAt,
                        UpdatedAt = dbOrder?.UpdatedAt,
                        IdCustomer = dbOrder.IdCustomerNavigation?.IdCustomer,
                        Fullname = dbOrder.IdCustomerNavigation?.Fullname,
                        PhoneNumber = dbOrder.IdCustomerNavigation?.PhoneNumber,
                        Gender = dbOrder.IdTableNavigation?.IdTable,
                        IdTable = dbOrder.IdTableNavigation?.IdTable,
                        NameTable = dbOrder.IdTableNavigation?.Name,
                        StatusTable = dbOrder.IdTableNavigation?.Status
                    };
                    if (dbOrder.IdCustomerNavigation?.Fullname != null && searchValue != null)
                    {
                        if (_Convert.ConvertToUnSign(dbOrder.IdCustomerNavigation.Fullname).Contains(_Convert.ConvertToUnSign(searchValue)))
                        {
                            data.Add(order);
                        }
                    }
                }
                var db = data.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
                if (dbOrders1.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";
                    return response;
                }
                response.TotalPage = data.Count();
                response.Data = db;
                return response;
            }
            if (typeSearch == "phonenumber")
            {

                var dbOrders2 = await _context.Orders.Include(o => o.IdTableNavigation).Include(o => o.IdCustomerNavigation).OrderByDescending(p => p.CreatedAt)
                                .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                                 .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                 .ToListAsync();
                var count2 = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                 .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                 .CountAsync();
                if (dbOrders2.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count2;
                response.Data = dbOrders2;
                return response;
            }
            if (typeSearch == "tableFood")
            {
                var dbOrders3 = await _context.Orders.Include(o => o.IdTableNavigation).Include(o => o.IdCustomerNavigation).OrderByDescending(p => p.CreatedAt)
                               .Where(o => o.IdTableNavigation.Name.ToString() == searchValue)
                               .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                              .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                              .ToListAsync();
                var count3 = await _context.Orders.Include(o => o.IdTableNavigation)
                              .Where(o => o.IdTableNavigation.Name.ToString() == searchValue).CountAsync();
                if (dbOrders3.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count3;
                response.Data = dbOrders3;
                return response;
            }
            var dbOrders = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                 .Include(o => o.IdTableNavigation)
                                 .OrderByDescending(p => p.CreatedAt)
                                 .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                                .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                 .ToListAsync();
            var count = await _context.Orders.CountAsync();
            if (dbOrders.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found order";

                return response;
            }
            response.Data = dbOrders;
            response.TotalPage = count;
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
                Amount = orderDto.Amount,
            };
            var dbTableFood = await _context.TableFoods.FindAsync(orderDto.IdTable);
            if (dbTableFood != null)
            {
                dbTableFood.Status = 1;
            }
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
            if (dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found";
                return response;
            }
            if (orderDto.Status == 1)
            {
                dbOrder.TimePay = DateTime.Now;
                var dbTable = await _context.TableFoods.FindAsync(dbOrder.IdTable);
                if(dbTable != null)
                {
                    dbTable.Status = 0;
                }

            }
            if (orderDto.Status == 0)
            {
                dbOrder.TimePay = null;
            }
            if (orderDto.IdTable != null)
            {
                var dbTable = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == dbOrder.IdTable);
                var dbNewTable = await _context.TableFoods.SingleOrDefaultAsync(tb => tb.IdTable == orderDto.IdTable);
                if (dbTable != null)
                {
                    dbTable.Status = 0;
                }
                if (dbNewTable != null)
                {
                    dbNewTable.Status = 1;
                }
            }
            _mapper.Map(orderDto, dbOrder);
            await _context.SaveChangesAsync();
            response.Data = dbOrder;
            return response;
        }

        public async Task<ApiResponse<AnyType>> DeleteOrder(string Id)
        {
            var response = new ApiResponse<AnyType>();
            var dbOrder = await _context.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.IdOrder == Id);

            if (dbOrder == null)
            {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            foreach (var item in dbOrder.OrderDetails)
            {
                _context.OrderDetails.Remove(item);
            }
            var dbTable = await _context.TableFoods.FindAsync(dbOrder.IdTable);
            if (dbTable != null)
            {
                dbTable.Status = 0;
            }
            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();
            return response;
        }


        public async Task<ApiResponse<List<OrderGet>>> GetOrderPaid(int page, string? typeSearch, string? searchValue)
        {
            var response = new ApiResponse<List<OrderGet>>();
            if (typeSearch == "nameCustomer")
            {
                var data = new List<OrderGet>();
                var dbOrders1 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 1).Include((o) => o.IdCustomerNavigation)
                                                 .Include(o => o.IdTableNavigation)
                                                 .ToListAsync();
                foreach (var dbO in dbOrders1)
                {
                    OrderGet order = new OrderGet()
                    {
                        IdOrder = dbO.IdOrder.ToString(),
                        Status = dbO.Status,
                        Amount = dbO.Amount,
                        TimePay = dbO.TimePay,
                        CreatedAt = dbO.CreatedAt,
                        UpdatedAt = dbO.UpdatedAt,
                        IdCustomer = dbO.IdCustomerNavigation?.IdCustomer,
                        Fullname = dbO.IdCustomerNavigation?.Fullname,
                        PhoneNumber = dbO.IdCustomerNavigation?.PhoneNumber,
                        Gender = dbO.IdTableNavigation?.IdTable,
                        IdTable = dbO.IdTableNavigation.IdTable,
                        NameTable = dbO.IdTableNavigation.Name,
                        StatusTable = dbO.IdTableNavigation.Status
                    };
                    if (dbO.IdCustomerNavigation?.Fullname != null && searchValue != null)
                    {
                        if (_Convert.ConvertToUnSign(dbO.IdCustomerNavigation.Fullname).Contains(_Convert.ConvertToUnSign(searchValue)))
                        {
                            data.Add(order);
                        }
                    }
                }
                var db = data.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
                if (dbOrders1.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";
                    return response;
                }
                response.TotalPage = data.Count();
                response.Data = db;
                return response;
            }
            if (typeSearch == "phonenumber")
            {

                var dbOrders2 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 1).Include(o => o.IdTableNavigation).Include(o => o.IdCustomerNavigation)
                                 
                                .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                                 .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                 .ToListAsync();
                var count2 = await _context.Orders.Where(o => o.Status == 1).Include(o => o.IdCustomerNavigation)
                                 .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                 .CountAsync();
                if (dbOrders2.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count2;
                response.Data = dbOrders2;
                return response;
            }
            if (typeSearch == "tableFood")
            {
                var dbOrders3 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 1).Include(o => o.IdTableNavigation).Include(o => o.IdCustomerNavigation).OrderByDescending(p => p.CreatedAt)

                               .Where(o => o.IdTableNavigation.Name.ToString() == searchValue)
                               .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                              .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                              .ToListAsync();
                var count3 = await _context.Orders.Where(o => o.Status == 1).Include(o => o.IdTableNavigation)
                              .Where(o => o.IdTableNavigation.Name.ToString() == searchValue).CountAsync();
                if (dbOrders3.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count3;
                response.Data = dbOrders3;
                return response;
            }
            var dbOrder = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 1).Include(o => o.IdCustomerNavigation)
                                .Include(o => o.IdTableNavigation)
                                .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 }).
                                ToListAsync();

            var count = await _context.Orders.Where(o => o.Status == 1).CountAsync();
            if (dbOrder.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found order";
                return response;
            }
            response.TotalPage = count;
            response.Data = dbOrder;
            return response;
        }


        public async Task<ApiResponse<List<OrderGet>>> GetOrderUnpaid(int page, string? typeSearch, string? searchValue, string? timeStart, string? timeEnd)
        {

            var response = new ApiResponse<List<OrderGet>>();
            DateTime? convertTimeStart = null;
            DateTime? convertTimeEnd = null;
            string format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
            //check time start and time end
            if (!string.IsNullOrEmpty(timeStart) && !string.IsNullOrEmpty(timeEnd))
            {
                convertTimeStart = DateTime.ParseExact(timeStart, format, CultureInfo.InvariantCulture);
                convertTimeEnd = DateTime.ParseExact(timeEnd, format, CultureInfo.InvariantCulture);
            }
            // Console.WriteLine("check time", +convertTimeStart);
            if (typeSearch == "nameCustomer")
            {
                var data = new List<OrderGet>();
                var dbOrders1 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 0)
                                                  .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                                                 .Include((o) => o.IdCustomerNavigation)
                                                 .Include(o => o.IdTableNavigation)
                                                 .ToListAsync();
                foreach (var dbOrder in dbOrders1)
                {
                    OrderGet order = new OrderGet()
                    {
                        IdOrder = dbOrder.IdOrder.ToString(),
                        Status = dbOrder?.Status,
                        Amount = dbOrder?.Amount,
                        TimePay = dbOrder?.TimePay,
                        CreatedAt = dbOrder?.CreatedAt,
                        UpdatedAt = dbOrder?.UpdatedAt,
                        IdCustomer = dbOrder?.IdCustomerNavigation?.IdCustomer,
                        Fullname = dbOrder?.IdCustomerNavigation?.Fullname,
                        PhoneNumber = dbOrder?.IdCustomerNavigation?.PhoneNumber,
                        Gender = dbOrder?.IdTableNavigation?.IdTable,
                        IdTable = dbOrder?.IdTableNavigation?.IdTable,
                        NameTable = dbOrder?.IdTableNavigation?.Name,
                        StatusTable = dbOrder?.IdTableNavigation?.Status
                    };
                    if (dbOrder?.IdCustomerNavigation?.Fullname != null && searchValue != null)
                    {
                        if (_Convert.ConvertToUnSign(dbOrder.IdCustomerNavigation.Fullname).Contains(_Convert.ConvertToUnSign(searchValue)))
                        {
                            data.Add(order);
                        }
                    }
                }
                var db = data.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
                if (dbOrders1.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";
                    return response;
                }
                response.TotalPage = data.Count();
                response.Data = db;
                return response;
            }
            if (typeSearch == "phonenumber")
            {

                var dbOrders2 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 0).Include(o => o.IdTableNavigation).Include(o => o.IdCustomerNavigation)
                                .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                                .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                                .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                 .ToListAsync();
                var count2 = await _context.Orders.Include(o => o.IdCustomerNavigation)
                                  .Where(o => o.Status == 0)
                                  .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                                 .Where(o => o.IdCustomerNavigation.PhoneNumber == searchValue)
                                 .CountAsync();
                if (dbOrders2.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count2;
                response.Data = dbOrders2;
                return response;
            }
            if (typeSearch == "tableFood")
            {
                var dbOrders3 = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 0)
                    .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                     .Include(o => o.IdTableNavigation)
                               .Include(o => o.IdCustomerNavigation)
                               .Where(o => o.IdTableNavigation.Name.ToString() == searchValue)
                               .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                             .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                              .ToListAsync();
                var count3 = await _context.Orders.Include(o => o.IdTableNavigation)
                               .Where(o => o.Status == 0)
                               .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                              .Where(o => o.IdTableNavigation.Name.ToString() == searchValue).CountAsync();
                if (dbOrders3.Count <= 0)
                {
                    response.Status = false;
                    response.Message = "Not found order";

                    return response;
                }
                response.TotalPage = count3;
                response.Data = dbOrders3;
                return response;
            }

            var dbOrders = await _context.Orders.OrderByDescending(p => p.CreatedAt).Where(o => o.Status == 0)
                                  .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                                  .Include(o => o.IdCustomerNavigation)
                                 .Include(o => o.IdTableNavigation)
                                 .Select(o =>
                                 new OrderGet
                                 {
                                     IdOrder = o.IdOrder.ToString(),
                                     Status = o.Status,
                                     Amount = o.Amount,
                                     TimePay = o.TimePay,
                                     CreatedAt = o.CreatedAt,
                                     UpdatedAt = o.UpdatedAt,
                                     IdCustomer = o.IdCustomerNavigation.IdCustomer,
                                     Fullname = o.IdCustomerNavigation.Fullname,
                                     PhoneNumber = o.IdCustomerNavigation.PhoneNumber,
                                     Gender = o.IdTableNavigation.IdTable,
                                     IdTable = o.IdTableNavigation.IdTable,
                                     NameTable = o.IdTableNavigation.Name,
                                     StatusTable = o.IdTableNavigation.Status
                                 })
                                .Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)
                                 .ToListAsync();
            var count = await _context.Orders
                 .Where(o => o.Status == 0)
                 .Where(o => (!convertTimeStart.HasValue || o.CreatedAt >= convertTimeStart) &&
                                    (!convertTimeEnd.HasValue || o.CreatedAt <= convertTimeEnd))
                .CountAsync();
            if (dbOrders.Count <= 0)
            {
                response.Status = false;
                response.Message = "Not found order";

                return response;
            }
            response.Data = dbOrders;
            response.TotalPage = count;
            return response;
        }
        public async Task<ApiResponse<AnyType>> GraftOrder(string IdOldOrder, string IdNewOrder){
            var response = new ApiResponse<AnyType>();
            var oldOrder = await _context.Orders.FindAsync(IdOldOrder);
            var newOrder = await _context.Orders.FindAsync(IdNewOrder);
            if (oldOrder == null || newOrder == null) {
                response.Status = false;
                response.Message = "Not found order";
                return response;    
            }
            if(oldOrder.IdTable != null)
            {
                var dbTable = await _context.TableFoods.FindAsync(oldOrder.IdTable);
               if(dbTable != null)
                {
                    dbTable.Status = 0;
                }
            }
           var orderDetailNewOrders = await _context.OrderDetails.Where(od=> od.IdOrder == IdNewOrder).ToListAsync();
           var orderDetailOldOrders = await _context.OrderDetails.Where(od=> od.IdOrder == IdOldOrder).ToListAsync();
            foreach (var itemNewOrder in orderDetailNewOrders)
            {
                var matchingItemOldOrder = orderDetailOldOrders.FirstOrDefault(itemOldOrder => itemOldOrder.IdProduct == itemNewOrder.IdProduct);
                if (matchingItemOldOrder != null)
                {
                    var amount = itemNewOrder.Amout + matchingItemOldOrder.Amout;
                    var price = itemNewOrder.Price + matchingItemOldOrder.Price;
                    itemNewOrder.Amout = amount;
                    itemNewOrder.Price = price;
                }
               
            }
            foreach(var item in orderDetailOldOrders)
            {
                var matchingItemNewOrder = orderDetailNewOrders.FirstOrDefault(itemNewOrder => itemNewOrder.IdProduct == item.IdProduct);
                if(matchingItemNewOrder == null)
                {
                    var newOrderDetail = new OrderDetail()
                    {
                        IdOrderDetail= Guid.NewGuid().ToString().Substring(0, 10),
                        IdOrder = IdNewOrder, // Gán IdOrder của order mới
                        IdProduct = item.IdProduct,
                        Amout = item.Amout,
                        Price = item.Price
                    };

                    _context.OrderDetails.Add(newOrderDetail);
                }

            }
            var priceNewOrder = oldOrder.Price + newOrder.Price;
            var amountCustomer = newOrder.Amount + oldOrder.Amount;
            newOrder.Amount = amountCustomer;
            newOrder.Price = priceNewOrder;
            _context.Orders.Remove(oldOrder);
            await _context.SaveChangesAsync();   

            return response;
        }
        public async Task<ApiResponse<AnyType>> SplitOrder(string IdOldOrder, string IdNewOrder, List<DataSplitOrder>? SplitOrders) {
            var response = new ApiResponse<AnyType>();
            var dbOldOrder = await _context.Orders.FindAsync(IdOldOrder);
            var dbNewOrder = await _context.Orders.FindAsync(IdNewOrder);
            if(dbOldOrder == null || dbNewOrder == null)
            {
                response.Status = false;
                response.Message = "Không tìm thấy hóa đơn";
                return response;
            }
            var orderDetailNewOrders = await _context.OrderDetails.Where(od => od.IdOrder == IdNewOrder).ToListAsync();
            var orderDetailOldOrders = await _context.OrderDetails.Where(od => od.IdOrder == IdOldOrder).ToListAsync();
            if (SplitOrders == null )
            {
                response.Status = false;
                response.Message = "Hãy thêm các món ăn muốn tách";
                return response;
            }
            foreach(var item in SplitOrders)
            {
                var dbProduct = await _context.Products.FindAsync(item.IdProduct);
                if(dbProduct == null)
                {
                    response.Status = false;
                    response.Message = "Không tìm thấy món ăn";
                    return response;
                }
                var matchingItemOldOrder = orderDetailOldOrders.FirstOrDefault(itemODt => itemODt.IdProduct == item.IdProduct);
                if(matchingItemOldOrder != null)
                {
                    var amount = matchingItemOldOrder.Amout - item.CountSplit;
                    var price = amount * dbProduct.Price;
                    matchingItemOldOrder.Amout = amount;
                    matchingItemOldOrder.Price = price;
                    if(amount == 0)
                    {
                        _context.OrderDetails.Remove(matchingItemOldOrder);
                    }
                }
                var matchItemNewOrder = orderDetailNewOrders.FirstOrDefault(itemODt => itemODt.IdProduct == item.IdProduct);
                if(matchItemNewOrder != null)
                {
                    var amount = item.CountSplit + matchItemNewOrder.Amout;
                    var price = amount * dbProduct.Price;
                    matchItemNewOrder.Amout = amount;
                    matchItemNewOrder.Price = price;
                    var newPrice = dbNewOrder.Price + item.CountSplit  * dbProduct.Price;
                    dbNewOrder.Price = (long?)newPrice;
                }
                else
                {
                   if(item.CountSplit > 0) {
                        var newOrderDetail = new OrderDetail
                        {
                            IdOrderDetail = Guid.NewGuid().ToString().Substring(0, 10),
                            IdOrder = dbNewOrder.IdOrder,
                            IdProduct = item.IdProduct,
                            Price = dbProduct.Price * item.CountSplit,
                            Amout = item.CountSplit
                        };
                        _context.OrderDetails.Add(newOrderDetail);
                        var price = dbNewOrder.Price + dbProduct.Price * item.CountSplit;
                        dbNewOrder.Price = (long?)price;
                    }
                }
            }
            var OrderDetailOldOrder = await _context.OrderDetails.
                Where(od => od.IdOrder == dbOldOrder.IdOrder).ToListAsync();
            var priceOldOrder = OrderDetailOldOrder.Sum(od => od.Price);
            dbOldOrder.Price = (long?)priceOldOrder;
           await _context.SaveChangesAsync();
            return response;
        }
    }

}
