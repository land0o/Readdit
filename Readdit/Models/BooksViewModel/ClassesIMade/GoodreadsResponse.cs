using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class GoodreadsResponse
    {
        [JsonProperty("search")]
        [XmlArrayAttribute("search")]
        [XmlArrayItem(ElementName = "search")]
        [XmlText]
        public Search Search { get; set; }
    }

}
