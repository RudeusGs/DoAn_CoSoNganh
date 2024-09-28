using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Interfaces
{
    public interface INotificationService : IServiceBase<Notification>
    {
        public Task<ApiResult> Add(AddNotificationModel model);
        public Task<ApiResult> Update(UpdateNotificationModel model);

    }
}
