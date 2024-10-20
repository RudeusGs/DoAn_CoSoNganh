using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using Microsoft.EntityFrameworkCore;


namespace DragonAcc.Service.Services
{
    public class AccountWebsiteService : BaseService, IAccountWebsiteService
    {
        public AccountWebsiteService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public Task<ApiResult> Add(User model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Users.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public Task<ApiResult> Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}
