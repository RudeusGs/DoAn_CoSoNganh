using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Payment;
using DragonAcc.Service.Models.Transaction;
using System.Transactions;

namespace DragonAcc.Service.Interfaces
{
    public interface IPaymentService : IServiceBase<Payment>
    {
        public Task<ApiResult> Add(AddPaymentModel model);
        public Task<ApiResult> Update(UpdatePaymentModel model);
    }
}
