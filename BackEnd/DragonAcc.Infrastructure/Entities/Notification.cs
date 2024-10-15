using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class Notification : EntityBase
    {
        public int? SenderId { get; set; } // ID của người gửi
        public int? ReceiverId { get; set; } // ID của người nhận
        public string? Title { get; set; }
        public string? Message { get; set; }
    }
}
