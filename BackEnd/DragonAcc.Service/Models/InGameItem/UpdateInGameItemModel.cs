﻿using Microsoft.AspNetCore.Http;
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
        public List<IFormFile>? Files { get; set; }
        public int? StarQ { get; set; }
        public string? Server { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
    }
}
