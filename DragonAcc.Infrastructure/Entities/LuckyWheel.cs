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
        public GameAccount? GameAccounts { get; set; }
        public InGameItem? InGameItems { get; set; }
        public float? Probability { get; set; }
        public int? UserId { get; set; }
    }
}
