using DragonAcc.Service.Models.AccountGame;
using DragonAcc.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonAcc.Service.Models.DailyCheckIn;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Infrastructure.Entities;

namespace DragonAcc.Service.Interfaces
{
    public interface IDailyCheckInService : IServiceBase<DailyCheckIn>
    {
        public Task<ApiResult> Add(AddDailyCheckInModel model); // admin them diem danh
        public Task<ApiResult> Add2(CheckInModel model); // user(admin-test) diem danh

        public Task<ApiResult> Update(UpdateDailyCheckInModel model);
    }
}
