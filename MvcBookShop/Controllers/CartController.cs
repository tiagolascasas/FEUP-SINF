using System;
using System.Data;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using MvcBookShop.Utility;

namespace MvcBookShop.Controllers
{

    public class CartController : Controller
    {

        public IActionResult Index()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            List<Book> booksOnCart = HttpContext.Session.GetObjectFromJson<List<Book>>("booksOnCart");

            ViewData["BooksOnCart"] = booksOnCart;

            if (booksOnCart == null || booksOnCart.Count == 0)
            {
                ViewData["BooksOnCart"] = new List<Book> { };
                ViewData["Error"] = "There are no books on cart. Add one!";
            }

            ViewData["Nome"] = HttpContext.Session.GetString("Nome");
            ViewData["Morada"] = HttpContext.Session.GetString("Morada");
            ViewData["CodigoPostal"] = HttpContext.Session.GetString("CodigoPostal");
            ViewData["NIF"] = HttpContext.Session.GetString("NIF");
            ViewData["Telefone"] = HttpContext.Session.GetString("Telefone");

            return View();
        }

        public IActionResult AddToCart(string id)
        {
            dynamic book = WebServicesManager.Instance.WS04_GetBookInformation(id);

            List<Book> booksOnCart = HttpContext.Session.GetObjectFromJson<List<Book>>("booksOnCart");

            if (booksOnCart == null)
                booksOnCart = new List<Book>();


            foreach (dynamic x in book.DataSet.Table)
            {
                string PriceWoIVA = String.Format("{0:0.##}", (float)x.PVP1 * 0.94);

                Book bookToAdd = new Book()
                {
                    ID = id,
                    Title = x.Descricao,
                    Price = x.PVP1,
                    PriceWoIVA = PriceWoIVA,
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

                var itemDuplicated = booksOnCart.SingleOrDefault(r => r.ID == id);

                if (itemDuplicated == null)
                {
                    bookToAdd.QuantityOnCart = 1;
                    booksOnCart.Add(bookToAdd);
                }
                else
                    itemDuplicated.QuantityOnCart += 1;

            }

            HttpContext.Session.SetObjectAsJson("booksOnCart", booksOnCart);

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult PayForItemsOnCart()
        {

            List<Book> booksOnCart = HttpContext.Session.GetObjectFromJson<List<Book>>("booksOnCart");

            if (booksOnCart == null)
            {
                ViewData["BooksOnCart"] = new List<Book> { };
                ViewData["Error"] = "There was an error retrieving the books of this category";
                return BadRequest("There was an error retrieving the books of this category");
            }
            else if (booksOnCart.Count == 0)
            {
                ViewData["Error"] = "There is no books on cart";
                return BadRequest("There was an error retrieving the books of this category");
            }
            else if (HttpContext.Session.GetString("username") == null)
            {
                ViewData["Error"] = "You need to Login first";
                return BadRequest("There was an error retrieving the books of this category");
            }

            string bodyDocument = "{ \"Linhas\": [";
            foreach (Book book in booksOnCart)
            {
                string bookString = "{ \"Artigo\": \"" + book.ID + "\", \"Quantidade\": \"" + book.QuantityOnCart + "\"},";
                bodyDocument += bookString;
            }
            bodyDocument += "], \"Tipodoc\": \"ECL\", \"Serie\": \"A\",\"Entidade\": \"" + HttpContext.Session.GetString("username") + "\", \"TipoEntidade\": \"C\", \"DataDoc\": \"19/12/2018\", \"DataVenc\": \"19/12/2018\" }";

            Console.Write(bodyDocument);

            dynamic orders = WebServicesManager.Instance.WS09_PlaceOrder(bodyDocument);

            booksOnCart = new List<Book>();
            HttpContext.Session.SetObjectAsJson("booksOnCart", booksOnCart);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveItemFromCart(string id)
        {
            List<Book> booksOnCart = HttpContext.Session.GetObjectFromJson<List<Book>>("booksOnCart");

            if (booksOnCart == null || booksOnCart.Count == 0)
            {
                ViewData["BooksOnCart"] = new List<Book> { };
                ViewData["Error"] = "There are no books on cart. Add one!";
                return BadRequest("There are no books on cart. Add one!");

            }

            var itemToRemove = booksOnCart.SingleOrDefault(r => r.ID == id);

            if (itemToRemove != null)
                booksOnCart.Remove(itemToRemove);

            HttpContext.Session.SetObjectAsJson("booksOnCart", booksOnCart);

            return RedirectToAction("Index", "Cart");
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
