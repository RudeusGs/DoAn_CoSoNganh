using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Dịch vụ game
    public class GameService : EntityBase
    {
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }

    }
}
