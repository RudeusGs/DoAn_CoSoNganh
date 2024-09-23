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
     * User: quay
     * API Endpoint Design: add, update, delete, getall
     */
    public class LuckyWheel : EntityBase
    {
        public string? Prize { get; set; }
        public float? Probability { get; set; }
        public string? Image { get; set; }
        public int? UserId { get; set; }
    }
}
