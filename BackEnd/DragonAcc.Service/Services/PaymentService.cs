using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Payment;
using DragonAcc.Service.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class PaymentService : BaseService, IPaymentService
    {
        public PaymentService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(AddPaymentModel model)
        {
            var payMent = await _dataContext.Payments.FirstOrDefaultAsync(x => x.Dentistry == model.Dentistry);

            if (payMent == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var newPayMent = new Payment
                    {
                        Dentistry = model.Dentistry,
                        Amount = model.Amount,
                        Discount = model.Discount,
                        CreatedDate = DateTime.Now,
                    };

                    _dataContext.Payments.Add(newPayMent);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newPayMent);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Phương thức thanh toàn này đã tồn tại!" };
        }

        public Task<ApiResult> Add(Payment model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Delete(int id)
        {
            var payMent = await _dataContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payMent != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.Payments.Remove(payMent);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Không tìm thấy nhà cung cấp này." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Payments.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(UpdatePaymentModel model)
        {
            var payMent = await _dataContext.Payments.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (payMent != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var existingService = await _dataContext.Payments
                        .FirstOrDefaultAsync(x => x.Dentistry == model.Dentistry && x.Id != payMent.Id);

                    if (existingService != null)
                    {
                        return new() { Message = "Tên nhà cung cấp đã tồn tại" };
                    }
                    payMent.Dentistry = model.Dentistry ?? payMent.Dentistry;
                    payMent.Amount = model.Amount ?? payMent.Amount;
                    payMent.Discount = model.Discount ?? payMent.Discount;
                    payMent.UpdatedDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new() { Message = "Cập nhật thành công!" };
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new() { Message = "Nhà cung cấp này không tồn tại!" };
        }

        public Task<ApiResult> Update(Payment model)
        {
            throw new NotImplementedException();
        }
    }
}
