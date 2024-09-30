using Microsoft.AspNetCore.Http;

namespace DragonAcc.Service.Models.GameService
{
    public class AddGameServiceModel
    {
        public int? Server { get; set; }
        public string? ServiceName { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
    }
}
