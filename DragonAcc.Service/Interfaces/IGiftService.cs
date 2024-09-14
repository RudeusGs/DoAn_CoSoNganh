using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Interfaces
{
    public interface IGiftService : IServiceBase<Gift>
    {
        public Task<ApiResult> Delete();
    }
}
