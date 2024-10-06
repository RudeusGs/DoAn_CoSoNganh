using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.DailyCheckIn
{
    public class AddDailyCheckInModel
    {
        public DateTime? CheckInDate { get; set; }
        public int? Coin { get; set; }
    }
}
