using System;
using System.ComponentModel.DataAnnotations;

namespace Readdit.Models.BooksViewModel
{
    [Serializable]
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
