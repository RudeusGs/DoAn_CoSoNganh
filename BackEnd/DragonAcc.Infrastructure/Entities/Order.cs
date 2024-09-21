using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Đơn hàng
     * API Endpoint Design: add, update, delete, getall, getbyid
     */
    public class Order : EntityBase
    {
        public int? UserId { get; set; }
        public string? TotalAmount { get; set; }
        public bool? Status { get; set; }

    }
}
