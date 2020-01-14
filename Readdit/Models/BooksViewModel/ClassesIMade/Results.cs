using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class Results
    {
        [JsonProperty("work")]
        [XmlArrayAttribute("work")]
        public List<Work> Work { get; set; }
    }
}