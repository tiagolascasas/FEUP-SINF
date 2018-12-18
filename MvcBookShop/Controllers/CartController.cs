using System;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using System.Text.Encodings.Web;
using Newtonsoft.Json.Linq;

namespace MvcBookShop.Controllers{

    public class CartController : Controller{
        
        public IActionResult Index(){

            
            return View();
        }

        /* public IActionResult AddToCart(string id)
        {
            dynamic book = WebServicesManager.Instance.WS04_GetBookInformation(id);

            List<Book> booksOnCart = (List<Book>) httpContext.Session.GetObjectFromJson<string>("sessionItem");

            foreach (dynamic x in book.DataSet.Table)
            {
                booksOnCart.Add(new Book()
                {
                    ID = id,
                    Title = x.Descricao,
                    Price = x.PVP1,
                    Author = x.CDU_Autor,
                    Sinopse = x.CDU_Sinopse,
                    ISBN = x.CDU_ISBN,
                    Editor = x.CDU_Editora,
                    Binding = x.CDU_Capa,
                    Pages = x.CDU_Paginas,
                    PublishYear = x.CDU_Ano,
                    Dimensions = x.CDU_Dimensoes,
                    Category = x.Familia,
                    Language = x.CDU_Idioma,
                    Cover = @"./images/books/" + id + ".jpg"
                });
            }


            return RedirectToAction("Index","HomeController");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error(){
            return View(
                new ErrorViewModel { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
                }
            );
        }
    }
}
