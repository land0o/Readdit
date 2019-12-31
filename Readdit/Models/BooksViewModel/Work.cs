using System;
using System.ComponentModel.DataAnnotations;

namespace Readdit.Models.BooksViewModel
{
    public class Work
    {

        public int Id { get; set; }

        public int original_publication_year { get; set; }

        public int original_publication_month { get; set; }

        public int original_publication_day { get; set; }

        public Best_Book Best_Book { get; set; }
    }
}
