using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;

namespace MvcBookShop.Controllers{

    public class HomeController : Controller{
        
        public IActionResult Index()
        {
            try
            {
                dynamic books = WebServicesManager.Instance.WS08_GetSetOfBooksOrderedByRelease();

                if (books != null)
                {
                    var bookList = new List<Book> { };

                    foreach (dynamic x in books.DataSet.Table)
                    {
                        if (bookList.Count < 6)
                            bookList.Add(new Book() { ID = x.Artigo, Title = x.Descricao, Price = x.PVP1, Author = x.CDU_Autor, Cover = @"./images/books/" + x.Artigo + ".jpg" });
                    }
                    ViewData["RecentBooks"] = bookList;
                    ViewData["RecentError"] = "";
                }
                else
                {
                    ViewData["RecentBooks"] = new List<Book> { };
                    ViewData["RecentError"] = "There was an error retrieving most recent books";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            
            try
            {
                dynamic books = WebServicesManager.Instance.WS07_GetSetOfBooksOrderedByStock();

                if (books != null)
                {
                    var bookList = new List<Book> { };
                    
                    foreach (dynamic x in books.DataSet.Table)
                    {
                        if (bookList.Count < 6)
                            bookList.Add(new Book() { ID = x.Artigo, Title = x.Descricao, Price = x.PVP1, Author = x.CDU_Autor, Cover = @"./images/books/" + x.Artigo + ".jpg" });
                    }

                    ViewData["PopularBooks"] = bookList;
                    ViewData["PopularError"] = "";
                }
                else
                {
                    ViewData["PopularBooks"] = new List<Book> { };
                    ViewData["PopularError"] = "There was an error retrieving most popular books";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            return View();
        }

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
