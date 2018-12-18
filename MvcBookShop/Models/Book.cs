using System;
using System.ComponentModel.DataAnnotations;

namespace MvcBookShop.Models
{
    public class Book
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public string Sinopse { get; set; }
        public string ISBN { get; set; }
        public string Editor { get; set; }
        public string Binding { get; set; }
        public string Pages { get; set; }
        public string PublishYear { get; set; }
        public string Dimensions { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Cover { get; set; }
    }
}
