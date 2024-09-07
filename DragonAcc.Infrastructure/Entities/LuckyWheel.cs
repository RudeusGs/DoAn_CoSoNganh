using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class LuckyWheel : EntityBase
    {
        public GameAccounts? _GameAccounts { get; set; }
        public InGameItems? _InGameItems { get; set; }
        public float? Probability { get; set; }
        public int? UserId { get; set; }
    }
}
