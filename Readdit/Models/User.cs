using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models
{
    public class User
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string imageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

    }
}
