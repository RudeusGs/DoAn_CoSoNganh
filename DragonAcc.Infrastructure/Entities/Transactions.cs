using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class Transactions : EntityBase
    {
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
        public string? Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public bool? Status { get; set; }

    }
}
