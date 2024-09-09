using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.AccountGame
{
    public class UpdateGameAccountModel
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
    }
}
