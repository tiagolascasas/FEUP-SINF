using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using Microsoft.AspNetCore.Http;

namespace MvcBookShop.Controllers
{

    public class OrderController : Controller
    {

        public IActionResult Index(int number)
        {
            Console.WriteLine("\n\n\n\n");
            ViewData["username"] = HttpContext.Session.GetString("username");

            if (HttpContext.Session.GetString("username") == null)
                return BadRequest("You need to be logged in order see your Orders");

            try
            {
                dynamic ECL = WebServicesManager.Instance.WS11_GetDocument(number);
                dynamic total = 0;

                if (HttpContext.Session.GetString("username") != (string)ECL.Entidade)
                    return BadRequest("You're not the owner of this order");
                List<string[]> artigosList = new List<string[]> { };
                foreach (dynamic x in ECL.Linhas)
                {
                    String[] artigos = new String[5];
                    artigos[0] = (string)x.Artigo;
                    artigos[1] = (string)x.Descricao;
                    artigos[2] = (string)x.Quantidade;
                    dynamic comIVA = x.PrecUnit + x.TotalIva;
                    artigos[3] = (string)x.PrecUnit;
                    artigos[4] = (string)comIVA;
                    total += ((float)comIVA * 100);
                    artigosList.Add(artigos);
                }
                ViewData["Artigos"] = artigosList;
                ViewData["Total"] = ((float)total / 100.0).ToString("0.00");
                ViewData["Entidade"] = (string)ECL.Entidade;
                ViewData["Pais"] = (string)ECL.Pais;
                ViewData["Nome"] = (string)ECL.Nome;
                ViewData["Morada"] = (string)ECL.Morada;
                ViewData["CodigoPostal"] = (string)ECL.CodigoPostal;
                ViewData["NumContribuinte"] = (string)ECL.NumContribuinte;
                ViewData["Estado"] = "";
                if ((string)ECL.Estado == "P")
                    ViewData["Estado"] = "Approved / pending";
                else if ((string)ECL.Estado == "T")
                    ViewData["Estado"] = "In transportation / Received";


                ViewData["ID"] = number;
                //Console.WriteLine(ECL);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            Console.WriteLine(number);
            Console.WriteLine("\n\n\n\n");
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