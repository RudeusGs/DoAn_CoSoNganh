
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace DragonAcc.Service.Models.LuckyWheel
{
    public class UpdateLuckyWheelModel
    {
        public int? Id { get; set; }
        public string? Prize { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? Image { get; set; }
        public float? Probability { get; set; }
    }
}
