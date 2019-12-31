using Newtonsoft.Json;
using System.Collections.Generic;

namespace Readdit.Models.BooksViewModel
{
    public class GoodreadsResponse
    {
        [JsonProperty("search")]
        public List<Search> Search { get; set; }
    }

}
