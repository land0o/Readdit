using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Readdit.Models.BooksViewModel
{
    public class Rootobject
    {
        public Xml xml { get; set; }
        public Goodreadsresponse GoodreadsResponse { get; set; }
    }

    public class Xml
    {
        public string version { get; set; }
        public string encoding { get; set; }
    }

    public class Goodreadsresponse
    {
        public Request Request { get; set; }
        public Search search { get; set; }
    }

    public class Request
    {
        public string authentication { get; set; }
        public Key key { get; set; }
        public Method method { get; set; }
    }

    public class Key
    {
        public string cdatasection { get; set; }
    }

    public class Method
    {
        public string cdatasection { get; set; }
    }

    public class Search
    {
        public Query query { get; set; }
        public string resultsstart { get; set; }
        public string resultsend { get; set; }
        public string totalresults { get; set; }
        public string source { get; set; }
        public string querytimeseconds { get; set; }
        public Results results { get; set; }
    }

    public class Query
    {
        public string cdatasection { get; set; }
    }

    public class Results
    {
        public Work[] work { get; set; }
    }

    public class Work
    {
        public Id id { get; set; }
        public Books_Count books_count { get; set; }
        public Ratings_Count ratings_count { get; set; }
        public Text_Reviews_Count text_reviews_count { get; set; }
        public Original_Publication_Year original_publication_year { get; set; }
        public Original_Publication_Month original_publication_month { get; set; }
        public Original_Publication_Day original_publication_day { get; set; }
        public string average_rating { get; set; }
        public Best_Book best_book { get; set; }
    }

    public class Id
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Books_Count
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Ratings_Count
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Text_Reviews_Count
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Original_Publication_Year
    {
        public string type { get; set; }
        public string text { get; set; }
        public string nil { get; set; }
    }

    public class Original_Publication_Month
    {
        public string type { get; set; }
        public string text { get; set; }
        public string nil { get; set; }
    }

    public class Original_Publication_Day
    {
        public string type { get; set; }
        public string text { get; set; }
        public string nil { get; set; }
    }

    public class Best_Book
    {
        public string type { get; set; }
        public Id1 id { get; set; }
        public string title { get; set; }
        public Author author { get; set; }
        public string image_url { get; set; }
        public string small_image_url { get; set; }
    }

    public class Id1
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Author
    {
        public Id2 id { get; set; }
        public string name { get; set; }
    }

    public class Id2
    {
        public string type { get; set; }
        public string text { get; set; }
    }

}
