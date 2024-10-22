
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;

namespace DragonAcc.Service.Interfaces
{
    public interface IPurchasedAccountService : IServiceBase<PurchasedAccount>
    {
        Task<ApiResult> GetAllByUser(int userId);

    }
}
