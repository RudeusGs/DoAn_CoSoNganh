using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Chi tiết giao dịch nạp tiền
     * API Endpoint Design: add, delete, getall, getbyid
     */
    public class TransactionDetail : EntityBase
    {
        public int? TransactionId { get; set; }
        public string? CardNumber { get; set; }
        public string? Amount { get; set; }
        public string? Discount {  get; set; }
        public string? Seri { get; set; }
        public string? Dentistry { get; set; }
    }
}
