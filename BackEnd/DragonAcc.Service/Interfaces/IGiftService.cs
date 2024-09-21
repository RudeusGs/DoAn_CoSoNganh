using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Models;
namespace DragonAcc.Service.Interfaces
{
    public interface IGiftService : IServiceBase<Gift>
    {
        public Task<ApiResult> Delete();
    }
}
