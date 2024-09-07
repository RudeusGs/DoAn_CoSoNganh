using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class Gifts
    {
        public string? GiftCode { get; set; }
        public float? Percent { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}
