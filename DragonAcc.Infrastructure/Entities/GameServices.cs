using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class GameServices : EntityBase
    {
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }

    }
}
