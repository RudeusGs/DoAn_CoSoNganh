
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace DragonAcc.Service.Models.LuckyWheel
{
    public class UpdateLuckyWheelModel
    {
        public int? Id { get; set; }
        public string? Prize { get; set; }
        public IFormFile? File { get; set; }
        public float? Probability { get; set; }
    }
}
