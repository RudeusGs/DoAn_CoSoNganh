using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Đánh giá webiste
     * API Endpoint Design: add, delete, getall
     */
    public class Review : EntityBase
    {
        public int? UserId { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
     }
}
