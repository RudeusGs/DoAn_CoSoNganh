using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Admin: thêm, sửa, xóa
     * User: mua
     * API Endpoint Design: add, update, delete, getall, getbyid
     */
    public class InGameItem : EntityBase
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemPrice { get; set; }
        public string? Image { get; set; }
        public int? StarQ { get; set; }
        public string? Server {  get; set; }
        public int? Quantity { get; set; }
    }
}
