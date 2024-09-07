using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Authenticate;

namespace DragonAcc.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ApiResult> Register(RegisterModel model);
        Task<ApiResult> Login(LoginModel model);
    }
}
