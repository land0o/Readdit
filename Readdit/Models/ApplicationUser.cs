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

            public Boolean isAdmin { get; set; }

            [NotMapped]
            [Display(Name = "Full Name")]
            public string FullName => $"{FirstName} {LastName}";
            public List<UserBook> userBooks { get; set; }
            //public List<Wishlist> wishlists { get; set; }
            //public List<Opinion> Opinion { get; set; }
        }
}
