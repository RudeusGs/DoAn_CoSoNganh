using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> GetCurrentUserIdAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName == null)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(userName);
            return user?.Id;
        }
    }
}
