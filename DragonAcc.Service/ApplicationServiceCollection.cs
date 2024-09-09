using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DragonAcc.Service
{
    public static class ApplicationServiceCollection
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Common services
            // JWT
            services.AddScoped<IJwtService, JwtService>();

            // User Management Service.
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddScoped<Common.IServices.IUserService, UserService>();

            // Ftp
            services.AddScoped<IFtpDirectoryService, FtpDirectoryService>();
            #endregion

            #region Business services
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IGameAccountService, GameAccountService>();
            /*services.AddScoped<I>*/
            #endregion
            return services;
        }
    }
}
