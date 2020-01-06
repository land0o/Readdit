using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class JsonResult
    {
        [JsonProperty("GoodreadsResponse")]
        [XmlArrayAttribute("GoodreadsResponse")]
        [XmlArrayItem(ElementName = "GoodreadsResponse")]
        public GoodreadsResponse GoodreadsResponse { get; set; }
    }
}
