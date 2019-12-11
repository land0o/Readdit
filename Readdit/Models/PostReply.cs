using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Readdit.Models
{
    public class PostReply
    {
        [Key]
        public int PostReplyId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Message { get; set; }
        public int PostId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}