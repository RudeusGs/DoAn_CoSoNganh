using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.InGameItem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class InGameItemService : BaseService, IIngameItemService
    {
        public InGameItemService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.InGameItems.ToListAsync() ;
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Add(AddInGameItemModel model)
        {
            var result = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.ItemName == model.ItemName);
            if (result == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var InGameItem = new InGameItem()
                    {
                        Server = model.Server,
                        ItemName = model.ItemName,
                        ItemDescription = model.ItemDescription,
                        ItemPrice = model.ItemPrice,
                        StarQ = model.StarQ,
                        Quantity = model.Quantity,
                        CreatedDate = _now

                    };
                    _dataContext.InGameItems.Add(InGameItem);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(InGameItem);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return new() { Message = "Danh mục game này đã có" };
        }

        public async Task<ApiResult> Update(InGameItem model)
        {
            var ingameitem = _dataContext.InGameItems.FirstOrDefault(x => x.Id == model.Id);
            if (ingameitem != null) 
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    ingameitem.Server = model.Server;
                    ingameitem.ItemName = model.ItemName;
                    ingameitem.ItemDescription = model.ItemDescription;
                    ingameitem.ItemPrice = model.ItemPrice;
                    ingameitem.StarQ = model.StarQ;
                    ingameitem.Quantity = model.Quantity;
                    ingameitem.UpdatedDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch(Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Không Tại Vật Phẩm Này!" }; 
            
        }
        public async Task<ApiResult> Delete(int id)
        {
            var ingameitem = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == id);
            if (ingameitem != null)
            {
                var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    ingameitem.DeleteDate = _now;
                    _dataContext.InGameItems.Remove(ingameitem);
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
            return new ApiResult() { Message = "Không Tìm Thấy Tài Khoản Này!" };
        }

        public async Task<ApiResult> Remove(int id)
        {
            var ingameitem = _dataContext.InGameItems.FirstOrDefault(x => x.Id == id);
            if (ingameitem != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    ingameitem.DeleteDate = _now;
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
            return new ApiResult() { Message = "Sản Phẩm Này Không Tồn Tại!" };
        }

        public Task<ApiResult> Update(AddInGameItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}
