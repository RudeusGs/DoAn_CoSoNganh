using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class OrderDetails : EntityBase
    { 
        public int? OrderId { get; set; }
        public int? GameAccountId { get; set; }
        public int? InGameItemId { get; set; }
        public int? AuctionId { get; set; }
        public int? LuckyWheel {  get; set; }
        public int? Quantity { get; set; }
        public string? UnitPrice { get; set; }
    }
}
