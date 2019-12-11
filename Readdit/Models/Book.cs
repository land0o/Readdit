using System.ComponentModel.DataAnnotations;

namespace Readdit.Models
{
    public class Book
    {
        public int id { get; set; }

        public int GoodreadsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string imageUrl { get; set; }

        public bool IsRead { get; set; }
        public bool IsOwned { get; set; }
        public bool IsWish { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}