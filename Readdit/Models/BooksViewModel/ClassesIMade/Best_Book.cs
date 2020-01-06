using System;
using System.ComponentModel.DataAnnotations;

namespace Readdit.Models.BooksViewModel.ClassesIMade
{
    [Serializable]
    public class Best_Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public string Image_Url { get; set; }
    }
}