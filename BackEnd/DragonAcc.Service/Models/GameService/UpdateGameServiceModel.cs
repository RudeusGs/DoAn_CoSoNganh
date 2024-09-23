using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.GameService
{
    public class UpdateGameServiceModel
    {
        public int? Id { get; set; }
        public int? Server { get; set; }
        public string? ServiceName { get; set; }
        public IFormFile? File { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
    }
}
