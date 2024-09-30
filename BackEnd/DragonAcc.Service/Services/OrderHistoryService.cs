/*using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.OrderHistory;
using Microsoft.EntityFrameworkCore;


namespace DragonAcc.Service.Services
{
    public class OrderHistoryService :  BaseService, IOrderHistoryService
    {
        private readonly DataContext _dataContext;

        public OrderHistoryService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
            _dataContext = dataContext;
        }

        public Task<ApiResult> Add(OrderHistory model)
        {
            throw new NotImplementedException();
        }

        // Phương thức để thêm lịch sử giao dịch
        public async Task<ApiResult> AddOrderHistory(AddOrderHistoryModel model)
        {
            if (model == null)
            {
                return new ApiResult
                {
                    Message = "Invalid order history data.",
                    IsSuccess = false
                };
            }

            var orderHistory = new OrderHistory
            {
                UserId = model.UserId,
                HistoryDate = DateTime.UtcNow
            };

            _context.OrderHistories.Add(orderHistory);
            await _context.SaveChangesAsync();

            return new ApiResult
            {
                Data = orderHistory,
                Message = "Order history added successfully.",
                IsSuccess = true
            };
        }

        public Task<ApiResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        // Phương thức lấy lịch sử giao dịch dựa trên OrderId
        public async Task<List<OrderHistory>> GetOrderHistoryFromOrderDetail(int orderId)
        {
            // Lấy danh sách OrderDetail dựa vào orderId
            var orderDetails = await _dataContext.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            // Nếu không tìm thấy chi tiết đơn hàng nào
            if (orderDetails == null || !orderDetails.Any())
            {
                return new List<OrderHistory>(); // Trả về danh sách trống nếu không có dữ liệu
            }

            var orderHistoryList = new List<OrderHistory>();

            foreach (var detail in orderDetails)
            {
                // Giả sử `OrderId` trong OrderDetail có liên kết với OrderHistory
                var orderHistory = await _dataContext.OrderHistories
                    .Where(oh => oh.Id == detail.OrderId) // Assuming Id matches OrderId in history
                    .FirstOrDefaultAsync();

                if (orderHistory != null)
                {
                    orderHistoryList.Add(orderHistory);
                }
            }

            return orderHistoryList; // Trả về danh sách lịch sử giao dịch
        }

        public Task<ApiResult> Update(OrderHistory model)
        {
            throw new NotImplementedException();
        }
    }
}
*/