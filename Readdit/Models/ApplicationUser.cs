using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models
{
    public class ApplicationUser : IdentityUser
    {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string LastName { get; set; }

            [Display(Name = "About me")]
            [MaxLength(75)]
            public string Description { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            public string imageUrl { get; set; }

            public bool isAdmin { get; set; }

            [NotMapped]
            [Display(Name = "Full Name")]
            public string FullName => $"{FirstName} {LastName}";
            public ICollection<Book> Books { get; set; }
            public ICollection<Forum> Forums { get; set; }
            public ICollection<Post> Posts { get; set; }
            public ICollection<PostReply> PostReplies { get; set; }
    }
}
