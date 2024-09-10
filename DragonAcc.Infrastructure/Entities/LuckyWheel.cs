using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Vòng quay may mắn
    public class LuckyWheel : EntityBase
    {
        public int? GameAccountId { get; set; }
        public int? InGameItemId { get; set; }
        public string? Price { get; set; }
        public float? Probability { get; set; }
        public int? UserId { get; set; }
    }
}
