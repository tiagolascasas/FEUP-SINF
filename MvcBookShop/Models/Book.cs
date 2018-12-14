using System;
using System.ComponentModel.DataAnnotations;

namespace MvcBookShop.Models
{
    public class Book
    {
        public string Artigo { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
    }
}
