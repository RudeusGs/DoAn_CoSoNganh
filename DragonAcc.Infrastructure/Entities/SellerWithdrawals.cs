using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class SellerWithdrawals : EntityBase
    {
        public string? WithdrawalAmount { get; set; }
        public string? WithdrawalStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
    }
}
