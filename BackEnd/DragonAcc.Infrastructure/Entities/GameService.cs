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
     * API Endpoint Design: add, update, delete, getall
     */
    public class GameService : EntityBase
    {
        public int? Server { get; set; }
        public string? ServiceName { get; set; }
        public string? Image {  get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
    }
}
