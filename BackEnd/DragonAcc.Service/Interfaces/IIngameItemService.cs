using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.InGameItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Interfaces
{
    public interface IIngameItemService : IServiceBase<InGameItem>
    {
        public Task<ApiResult> Remove(int id);
        public Task<ApiResult> Add(AddInGameItemModel model);
        public Task<ApiResult> Update(UpdateInGameItemModel model);
    }
}
