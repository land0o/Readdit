﻿using Readdit.Models.BooksViewModel;
using System.ComponentModel.DataAnnotations;

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
        
    }
}