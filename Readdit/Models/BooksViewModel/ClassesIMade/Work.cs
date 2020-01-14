using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class Work
    {

        public int Id { get; set; }

        public int original_publication_year { get; set; }

        public int original_publication_month { get; set; }

        public int original_publication_day { get; set; }

        [JsonProperty("best_book")]
        public List<Best_Book> Best_Book { get; set; }
    }
}
