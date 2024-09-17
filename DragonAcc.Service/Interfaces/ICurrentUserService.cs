using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Interfaces
{
    public interface ICurrentUserService
    {
        Task<int?> GetCurrentUserIdAsync();
    }
}
