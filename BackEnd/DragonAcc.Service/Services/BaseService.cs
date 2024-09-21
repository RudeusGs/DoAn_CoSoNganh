using DragonAcc.Infrastructure;
using DragonAcc.Service.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class BaseService
    {
        protected readonly DataContext _dataContext;
        protected readonly DateTime _now = DateTime.UtcNow.AddHours(7);
        protected readonly IUserService _userService;
        protected readonly string _userName;    
        public BaseService(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _userName = _userService.UserName;
        }
    }
}
