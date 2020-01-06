using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    public class BooksResponse
    {
        [Key]
       public int Books_Response_Id { get; set; }

       [Display(Name = "Year")]
       public int original_publication_year { get; set; }

       [Display(Name = "Month")]
       public int original_publication_month { get; set; }

       [Display(Name = "Day")]
       public int original_publication_day { get; set; }

       [Display(Name = "Ratings")]
       public int average_rating { get; set; }
       public string title { get; set; }
       public int Author_Id { get; set; }

       [Display(Name = "Author")]
       public string Author_Name { get; set; }
       public string image_url { set; get; }
       public Book book { get; set; }

    }
    


}
