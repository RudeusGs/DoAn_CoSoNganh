using DragonAcc.Infrastructure.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DragonAcc.Infrastructure.Entities
{
    public class Notification : EntityBase
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }

    }
}
