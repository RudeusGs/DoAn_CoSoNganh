using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure.Extensions;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(DataContext context, IUserService userService) : base(context, userService) { }

        public async Task<ApiResult> Add(Order model)
        {
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                model.CreatedDate = _now;
                _dataContext.Orders.Add(model);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new(model);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.InnerException?.Message ?? e.Message);
            }
        }


        public async Task<ApiResult> CreateNew(OrderModel model)
        {
            if (model.OrderDetails == null || model.OrderDetails.Count == 0)
            {
                return new() { Message = "Không có sản phẩm nào được chọn" };
            }

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                var order = new Order()
                {
                    CreatedDate = _now,
                    UserId = model.UserId,
                    TotalAmount = model.TotalAmount,
                    Status = model.Status
                };
                _dataContext.Orders.Add(order);
                await _dataContext.SaveChangesAsync();

                List<OrderDetail> orderDetails = new();
                if (order.Id != null)
                {
                    foreach (var item in model.OrderDetails)
                    {
                        var newItem = new OrderDetail()
                        {
                            OrderId = order.Id ?? 0,
                            GameAccountId = item.GameAccountId,
                            InGameItemId = item.InGameItemId,
                            AuctionId = item.AuctionId,
                            LuckyWheelId = item.LuckyWheelId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.TotalPrice,
                            CreatedDate = _now
                        };
                        orderDetails.Add(newItem);
                    }

                    _dataContext.OrderDetails.AddRange(orderDetails);
                    await _dataContext.SaveChangesAsync();
                }

                await tran.CommitAsync();
                return new();
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResult> Delete(int id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    order.DeleteDate = _now;
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
            return new ApiResult() { Message = "Order này không tồn tại" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Orders.Exist().ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Orders.Exist().FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> GetOrderByUserId()
        {
            var result = await _dataContext.Orders.Exist()
                .Where(x => x.UserId == _userService.UserId)
                .ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> Update(Order model)
        {
            var order = await _dataContext.Orders.Exist().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (order != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    order.UserId = model.UserId;
                    order.TotalAmount = model.TotalAmount;
                    order.Status = model.Status;
                    order.UpdatedDate = _now;
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
            return new ApiResult() { Message = "Order này không tồn tại" };
        }
    }
}
