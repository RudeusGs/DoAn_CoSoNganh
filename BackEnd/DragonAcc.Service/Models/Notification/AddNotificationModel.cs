using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Notification
{
    public class AddNotificationModel
    {
        public int? SenderId { get; set; } // ID của người gửi
        public int? ReceiverId { get; set; } // ID của người nhận
        public string? Title { get; set; }
        public string? Message { get; set; }
    }
}
