using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class Notification : EntityBase
    {
        public int? UserId { get; set; } 
        public string? Title { get; set; } 
        public string? Message { get; set; } 
        public string? NotificationType { get; set; }
    }
}
