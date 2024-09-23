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

        public Task<ApiResult> Update(AddInGameItemModel model)
        {

            throw new NotImplementedException();
        }
        //public async Task<ApiResult> Delete(int id)
        //{
        //    var result = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == id);
        //    if (result != null)
        //    {

        //    }
        //}
    }
}
