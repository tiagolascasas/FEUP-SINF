using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using Microsoft.AspNetCore.Http;


namespace MvcBookShop.Controllers
{

    public class OrdersController : Controller
    {
        
        public IActionResult Index(string username)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            Console.WriteLine("\n\n\n\n");
            try
            {
                dynamic ECLMatch = WebServicesManager.Instance.WS12_ListSeries();
                int numberOfECL = Int32.Parse(ECLMatch);

                List<Orders> orders = new List<Orders>();
                for (int i = numberOfECL; i > 0; i--)
                {
                    dynamic ECL = WebServicesManager.Instance.WS11_GetDocument(i);

                    if ((string)ECL.Entidade != username)
                        continue;
                    dynamic total = 0;
                    List<string[]> artigosList = new List<string[]> { };
                    foreach (dynamic x in ECL.Linhas)
                    {
                        String[] artigos = new String[2];
                        artigos[0] = (string)x.Artigo;
                        artigos[1] = (string)x.Descricao;
                        total += ((float)x.PrecUnit * 100);
                        artigosList.Add(artigos);
                    }

                    string estado = "";
                    if ((string)ECL.Estado == "P")
                        estado = "Approved / pending";
                    else if ((string)ECL.Estado == "T")
                        estado = "In transportation / Received";
                    orders.Add(new Orders() { artigos = artigosList, Total = ((float)total / 100.0).ToString("0.00"), DataCarga = (string)ECL.DataCarga, Estado = estado, orderNumber=i.ToString()});

                }
            ViewData["Orders"]=orders;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
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