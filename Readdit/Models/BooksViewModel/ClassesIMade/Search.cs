using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class Search
    {
        [JsonProperty("results")]
        [XmlArrayItem(ElementName = "result")]
        [XmlArray(ElementName = "results")]
        public Results Results { get; set; }

    }
}