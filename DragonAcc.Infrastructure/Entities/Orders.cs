using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class Orders : EntityBase
    {
        public int? UserId { get; set; }
        public string? TotalAmount { get; set; }
        public bool? Staus { get; set; }

    }
}
