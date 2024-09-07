using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class InGameItems : EntityBase
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemPrice { get; set; }
        public string? Server {  get; set; }
        public int? Quantity { get; set; }
    }
}
