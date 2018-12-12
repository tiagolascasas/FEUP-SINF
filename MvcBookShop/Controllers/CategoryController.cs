using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;

namespace MvcBookShop.Controllers{

    public class CategoryController : Controller{
        
        public IActionResult Index(){
            return View();
        }
        public IActionResult Romance() {
            dynamic books = WebServicesManager.Instance.WS05_GetSetOfBooksInCategory("Romance");

            ViewData["Books"] = books.DataSet.Table;

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