using DragonAcc.Infrastructure.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Admin: thêm, sửa, xóa
     * API Endpoint Design: add, update, delete, getall
     */
    public class Notification : EntityBase
    {
        public int? SenderId { get; set; } // ID của người gửi
        public int? ReceiverId { get; set; } // ID của người nhận
        public string? Title { get; set; }
        public string? Message { get; set; }
    }
}
