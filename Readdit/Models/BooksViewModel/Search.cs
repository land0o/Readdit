using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static Readdit.Models.BooksViewModel.Response;

namespace Readdit.Models.BooksViewModel
{
    public class Search
    {
        private GoodreadsResponseSearch searchField;

        //[XmlArrayItem("Results", typeof(Results))]
        //[XmlArrayAttribute("Results")]
        public Results Results { get; set; }
        public List<Work> Work { get; set; } = new List<Work>();

        public GoodreadsResponseSearch search
        {
            get
            {
                return this.searchField;
            }
            set
            {
                this.searchField = value;
            }
        }

    }
}