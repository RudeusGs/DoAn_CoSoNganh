using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.AccountGame;

namespace DragonAcc.Service.Interfaces
{
    public interface IGameAccountService : IServiceBase<GameAccount>
    {
        public Task<ApiResult> Add(AddGameAccountModel model);
        public Task<ApiResult> Update(UpdateGameAccountModel model);
        public Task<ApiResult> GetAll2();
    }
}
