using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Authenticate
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Balance { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
