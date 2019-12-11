using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Readdit.Models
{
    public class Forum
    {
        [Key]
        public int ForumId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        //public ICollection<ForumLike> ForumLikes { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}