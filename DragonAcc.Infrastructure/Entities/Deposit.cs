using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Mua hàng trả góp
    public class Deposit : EntityBase
    {
        public int? UserId { get; set; }
        public int? GameAccountId { get; set; }
        public int? InGameItemId { get; set; }
        public string? DepositAmount { get; set; }
        public DateTime? DepositDate { get; set; }
        public string? DepositStatus { get; set; }
    }
}
