using Microsoft.AspNetCore.Http;

namespace DragonAcc.Service.Models.AccountGame
{
    public class AddGameAccountModel
    {
        public string? Name { get; set; }
        public string? AccountName { get; set; }
        public string? AccountPassword { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
        public string? Status { get; set; }
        public bool? Earring { get; set; }
        public string? Planet { get; set; }
        public int? Server { get; set; }
        public DateTime? Created { get; set; }
    }
}
