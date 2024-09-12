using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Giao dịch nạp tiền
    public class Transaction : EntityBase
    {
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
        public string? PaymentMethod { get; set; }         
        public bool? Status { get; set; }

    }
}
