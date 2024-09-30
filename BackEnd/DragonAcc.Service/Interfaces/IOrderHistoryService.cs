using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.OrderHistory;

namespace DragonAcc.Service.Interfaces
{
    public interface IOrderHistoryService : IServiceBase<OrderHistory>
    {
        Task<ApiResult> AddOrderHistory(AddOrderHistoryModel model);
        Task<List<OrderHistory>> GetOrderHistoryFromOrderDetail(int orderId);
    }
}
