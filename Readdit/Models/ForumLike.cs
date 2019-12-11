using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models
{
    public class ForumLike
    {
        public int id { get; set; }
        public string ForumId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
