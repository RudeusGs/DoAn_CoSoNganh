using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Recharger;

namespace DragonAcc.Service.Interfaces
{
    public interface IRechargerService : IServiceBase<Recharger>
    {
        Task<ApiResult> Add(AddRechargerModel model);
        Task<ApiResult> Update(UpdateStatusRechargerModel model);
    }
}
