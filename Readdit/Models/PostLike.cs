namespace Readdit.Models
{
    public class PostLike
    {
        public int id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}