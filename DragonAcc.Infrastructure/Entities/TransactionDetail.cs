using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class TransactionDetail : EntityBase
    {
        public int? TransactionId { get; set; }
        public string? NumberCard { get; set; }
        public string? NumberSeri { get; set; }
        public string? NetworkOperator { get; set; }
        public string? Status { get; set; }
        public string? Code { get; set; }
    }
}
