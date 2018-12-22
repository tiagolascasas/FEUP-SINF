using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.PrimaveraWebServices;
using Newtonsoft.Json;

namespace MvcBookShop.Controllers
{
    public class EditUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditMorada(string morada)
        {
            EditField("Fac_Mor", morada);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPostalCode(string code)
        {
            EditField("Fac_Cp", code);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPhone(string phone)
        {
            EditField("Fac_Tel", phone);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditEmail(string email)
        {
            EditField("CDU_email", email);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPassword(string password)
        {
            string hash = AuthController.GetHash(SHA256.Create(), password);
            EditField("CDU_password_hash", hash);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public bool EditField(string field, string value)
        {
            Console.Write("\n\n\n\n\n" + field + " -> " + value + "\n\n\n\n\n");

            string user = HttpContext.Session.GetString("username");
            dynamic info = WebServicesManager.Instance.WS02_GetCustomerInformation(user);
            
            switch (field)
            {
                case "Fac_Mor":
                    info.Morada = value;
                    break;
                case "Fac_Cp":
                    info.CodigoPostal = value;
                    break;
                case "Fac_Tel":
                    info.Telefone = value;
                    Console.Write("\n\n\n\n\n" + "here" + "\n\n\n\n\n");
                    break;
                case "CDU_email":
                    foreach (var f in info.CamposUtil)
                    {
                        if (f.Nome == "CDU_email")
                        {
                            f.Conteudo = "ValorValor" + value;
                            f.Valor = value;
                        }
                    }
                    break;
                case "CDU_password_hash":
                    foreach (var f in info.CamposUtil)
                    {
                        if (f.Nome == "CDU_password_hash")
                        {
                            Console.Write("\n\n\\nIn password\n\n\n");
                            f.Conteudo = "ValorValor" + value;
                            f.Valor = value;
                        }
                    }
                    break;
                default:
                    break;
            }

            return WebServicesManager.Instance.WS03_UpdateCustomerAttributes(JsonConvert.SerializeObject(info));
        }
    }
}