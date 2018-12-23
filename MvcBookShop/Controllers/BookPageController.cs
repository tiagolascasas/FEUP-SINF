using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using System.Text.Encodings.Web;
using Newtonsoft.Json.Linq;

namespace MvcBookShop.Controllers
{

    public class BookPageController : Controller
    {

        public IActionResult Index(string id)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            dynamic book = WebServicesManager.Instance.WS04_GetBookInformation(id);

            var bookList = new List<Book> { };
            foreach (dynamic x in book.DataSet.Table)
            {
                string PriceCIVA = String.Format("{0:0.##}", (float)x.PVP1 * 1.06);
                Book b = new Book()
                {
                    ID = id,
                    Title = x.Descricao,
                    Price = PriceCIVA,
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
                };
                bookList.Add(b);
            }

            if (bookList.Count > 0)
            {
                ViewData["Title"] = bookList.First().Title;
                ViewData["Book"] = bookList.First();
                return View();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
