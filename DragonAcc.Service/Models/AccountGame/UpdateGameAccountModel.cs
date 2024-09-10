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
        public string? AccountName { get; set; }
        public string? AccountPassword { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
        public string? Status { get; set; }
        public bool? Earring { get; set; }
        public string? Planet { get; set; }
        public int? Server { get; set; }
    }
}
