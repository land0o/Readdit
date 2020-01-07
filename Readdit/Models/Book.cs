using Newtonsoft.Json;
using Readdit.Models.BooksViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Readdit.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string GoodreadsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string imageUrl { get; set; }

        public bool IsRead { get; set; }
        public bool IsOwned { get; set; }
        public bool IsWish { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [NotMapped]
        public List<Rootobject> Rootobject { get; set; }


    }
}