using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MvcBookShop.Models
{
    public class Orders
    {
        public List<string[]> artigos { get; set; }
        public string DataCarga { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }
        public string orderNumber { get; set; }
    }
}
