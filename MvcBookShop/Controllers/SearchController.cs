using System;
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




namespace MvcBookShop.Controllers
{

    public class SearchController : Controller
    {

        public IActionResult Index(string term)
        {

            ViewData["SearchTerm"] = term;

            try
            {

                dynamic books = WebServicesManager.Instance.WS06_SearchByTitle(term);

                var bookList = new List<Book> { };
                foreach (dynamic x in books.DataSet.Table)
                {
                    bookList.Add(new Book() { ID = x.Artigo, Title = x.Descricao, Price = x.PVP1, Author = x.CDU_Autor, Cover = @"./images/books/" + x.Artigo + ".jpg" });
                }

                ViewData["Books"] = bookList;

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
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