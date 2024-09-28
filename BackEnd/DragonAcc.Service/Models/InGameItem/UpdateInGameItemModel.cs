using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.InGameItem
{
    public class UpdateInGameItemModel
    {
        public int? Id { get; set; }    
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemPrice { get; set; }
        public IFormFile? File { get; set; }
        public int? StarQ { get; set; }
        public string? Server { get; set; }
        public int? Quantity { get; set; }
    }
}
