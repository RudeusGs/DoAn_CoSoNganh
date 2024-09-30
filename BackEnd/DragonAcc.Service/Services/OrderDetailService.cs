using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure.Extensions;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.OrderDetail;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class OrderDetailService : BaseService, IOrderDetailService
    {
        public OrderDetailService(DataContext context, IUserService userService) : base(context, userService) { }

        public Task<ApiResult> Add(OrderDetail model)
        {
            throw new NotImplementedException();
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

        public async Task<ApiResult> GetOrderDetailByOrderId(int orderId)
        {
            var result = await _dataContext.OrderDetails.Exist()
                .Where(x => x.OrderId == orderId)
                .Select(od => new OrderDetailModel
                {
                    OrderId = od.OrderId,
                    GameAccountId = od.GameAccountId,
                    InGameItemId = od.InGameItemId,
                    AuctionId = od.AuctionId,
                    LuckyWheelId = od.LuckyWheelId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    TotalPrice = od.TotalPrice,
                    Created = od.CreatedDate
                })
                .ToListAsync();

            return new ApiResult(result);
        }

        public Task<ApiResult> Update(OrderDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
