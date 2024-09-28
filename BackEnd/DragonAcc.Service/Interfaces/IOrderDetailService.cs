using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;


namespace DragonAcc.Service.Interfaces
{
    public interface IOrderDetailService : IServiceBase<OrderDetail>
    {
        Task<ApiResult> GetOrderDetailByOrderId(int orderId);
    }
}
