using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.AppUserViewModel
{
    public class UserImageViewModel
    {
        public IFormFile image { set; get; }
        public ApplicationUser User { get; set; }
    }
}
