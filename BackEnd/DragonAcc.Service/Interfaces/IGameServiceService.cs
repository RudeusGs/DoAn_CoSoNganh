using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models.AccountGame;
using DragonAcc.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonAcc.Service.Models.GameService;

namespace DragonAcc.Service.Interfaces
{
    public interface IGameServiceService : IServiceBase<GameService>
    {
        public Task<ApiResult> Add(AddGameServiceModel model);
        public Task<ApiResult> Update(UpdateGameServiceModel model);
        public Task<ApiResult> GetAll2();
    }
}
