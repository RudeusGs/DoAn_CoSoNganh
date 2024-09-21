using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Đánh giá webiste
     * API Endpoint Design: add, delete, getall
     */
    public class Review : EntityBase
    {
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
     }
}
