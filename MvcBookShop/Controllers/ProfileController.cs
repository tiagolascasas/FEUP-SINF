using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;

namespace MvcBookShop.Controllers{

    public class ProfileController : Controller{
        
        public IActionResult Index(string id){
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["id"] = id;
            HttpContext.Session.SetString("ID_USER", id);

            try
            {
                dynamic json = WebServicesManager.Instance.WS02_GetCustomerInformation(id);
                
                ViewData["Nome"] = json.Nome;
                ViewData["Morada"] = json.Morada;
                ViewData["CodigoPostal"] = json.CodigoPostal;
                ViewData["NIF"] = json.NumContribuinte;
                ViewData["Telefone"] = json.Telefone;
                ViewData["Email"] = json.CamposUtil[3].Valor;
                ViewData["Image"] = "./images/clients/" + id + ".jpg";
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