using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBookShop.Models
{
    public class User
    {
        public string Cliente;
        public string Nome;
        public string Morada;
        public string CodigoPostal;
        public string Telefone;
        public string NumContribuinte;
        public string Pais = "PT";
        public string Moeda = "EUR";

        public User() { }
    }
}
