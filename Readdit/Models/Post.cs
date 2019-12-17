using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Readdit.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        public Forum Forum { get; set; }
        public int ForumId { get; set; }
        public virtual IEnumerable<PostReply> PostReplies { get; set; }
        //public ICollection<PostLike> PostLikes { get; set; }
    }
}