using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models.Order;
using DragonAcc.Service.Models;


namespace DragonAcc.Service.Interfaces
{
    public interface IOrderService : IServiceBase<Order>
    {
        Task<ApiResult> CreateNew(OrderModel model);
        Task<ApiResult> GetOrderByUserId();
    }
}
