using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.LuckyWheel;

namespace DragonAcc.Service.Interfaces
{
    public interface ILuckyWheelService : IServiceBase<LuckyWheel>
    {
        public Task<ApiResult> Add(AddLuckyWheelModel model);
        public Task<ApiResult> Update(UpdateLuckyWheelModel model);
    }
}
