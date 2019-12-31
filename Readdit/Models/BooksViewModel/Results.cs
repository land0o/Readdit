using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Readdit.Models.BooksViewModel
{
    public class Results
    {
        //public Work Work { get; set; }

        [JsonProperty("work")]
        public Work Work { get; set; }
    }
}