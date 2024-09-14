using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Mã quà tặng
    public class Gift : EntityBase
    {
        public string? GiftCode { get; set; }
        public float? Percent { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
