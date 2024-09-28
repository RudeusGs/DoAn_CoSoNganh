using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Recharger
{
    public class AddRechargerModel
    {
        public string? Dentistry { get; set; }
        public string? Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? CardSeri { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
    }
}
