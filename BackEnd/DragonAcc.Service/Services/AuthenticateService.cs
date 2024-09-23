using DragonAcc.Infrastructure.Constants;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.Authenticate;
using DragonAcc.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DragonAcc.Service.Services
{
    public class AuthenticateService :IAuthenticateService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _dbContext;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(
            DataContext dbContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<ApiResult> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && !user.DeleteDate.HasValue)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                    if (signInResult.Succeeded)
                    {

                        var role = await _userManager.GetRolesAsync(user);
                        var roleString = String.Join(",", role.ToArray());
                        var tokenResult = GenerateUserToken(user, roleString);
                        if (tokenResult != null)
                        {
                            return new ApiResult()
                            {
                                Data = tokenResult,
                            };
                        }
                    }
                }

                return new ApiResult()
                {
                    Message = "Unorthorize",
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<ApiResult> Register(RegisterModel model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var userExist = await _userManager.FindByNameAsync(model.UserName);
                if (userExist != null)
                {
                    return new ApiResult()
                    {
                        Message = $"{model.Email} đã được sử dụng. Vui lòng thử lại!"
                    }; ;
                }

                User user = new User()
                {
                    CreatedDate = DateTime.Now,
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    Coin = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Balance = "0",
                    
                };

                var newUserResult = await _userManager.CreateAsync(user, model.Password);

                if (!newUserResult.Succeeded)
                {
                    var err = newUserResult.Errors.Select(x => x.Description);
                    return new() { Message = string.Join('\n', err) };
                }

                if (!await _roleManager.RoleExistsAsync(RoleConstants.USER.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>(RoleConstants.USER.ToString()));
                }

                await _userManager.AddToRoleAsync(user, RoleConstants.USER.ToString());

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ApiResult()
                {
                };

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.ToString());
            }
        }


        private UserToken GenerateUserToken(User user, string role, bool isExternalLogin = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var expires = DateTime.UtcNow.AddHours(7).AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.AddHours(7).ToString()),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),

                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["Jwt:ValidIssuer"],
                Audience = _configuration["Jwt:ValidAudience"]
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Token = token,
                Expires = expires
            };
        }
    }
}
