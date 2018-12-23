using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcBookShop.Models;
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

        public IActionResult Register()
        {
            try
            {
                User user = new User
                {
                    Cliente = Request.Form["username"],
                    Nome = Request.Form["name"],
                    Morada = Request.Form["address"],
                    NumContribuinte = Request.Form["nif"],
                    CodigoPostal = Request.Form["code"],
                    Telefone = Request.Form["phone"]
                };

                string data = JsonConvert.SerializeObject(user);

                Console.Write("\n\n\n\n" + data + "\n\n\n\n");

                bool success = WebServicesManager.Instance.WS01_CreateCustomer(data);
                if (!success)
                    return BadRequest("Problem creating user");

                HttpContext.Session.SetString("username", Request.Form["username"]);
                HttpContext.Session.SetString("Nome", Request.Form["name"]);
                HttpContext.Session.SetString("Morada", Request.Form["address"]);
                HttpContext.Session.SetString("CodigoPostal", Request.Form["code"]);
                HttpContext.Session.SetString("NIF", Request.Form["nif"]);
                HttpContext.Session.SetString("Telefone", Request.Form["phone"]);

                var editParams = new Dictionary<string, string>
                {
                    {"CDU_email", Request.Form["email"] },
                    {"CDU_password_hash", AuthController.GetHash(SHA256.Create(), Request.Form["password"]) },
                    {"CondPag", "1" },
                    {"ModoPag", "MB" }
                };

                EditFields(editParams);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                HttpContext.Session.Clear();
                return BadRequest("Error creating user");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditMorada(string morada)
        {
            EditFieldSingle("Fac_Mor", morada);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPostalCode(string code)
        {
            EditFieldSingle("Fac_Cp", code);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPhone(string phone)
        {
            EditFieldSingle("Fac_Tel", phone);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditEmail(string email)
        {
            EditFieldSingle("CDU_email", email);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public IActionResult EditPassword(string password)
        {
            string hash = AuthController.GetHash(SHA256.Create(), password);
            EditFieldSingle("CDU_password_hash", hash);
            return Redirect("/Profile?id=" + HttpContext.Session.GetString("username"));
        }

        public bool EditFieldSingle(string key, string val)
        {
            var d = new Dictionary<string, string>
            {
                { key, val }
            };
            return EditFields(d);
        }

        public bool EditFields(Dictionary<string, string> fields)
        {
            string user = HttpContext.Session.GetString("username");
            dynamic info = WebServicesManager.Instance.WS02_GetCustomerInformation(user);

            foreach (KeyValuePair<string, string> entry in fields)
            {
                switch (entry.Key)
                {
                    case "Fac_Mor":
                        info.Morada = entry.Value;
                        break;
                    case "Fac_Cp":
                        info.CodigoPostal = entry.Value;
                        break;
                    case "Fac_Tel":
                        info.Telefone = entry.Value;
                        break;
                    case "CDU_email":
                        foreach (var f in info.CamposUtil)
                        {
                            if (f.Nome == "CDU_email")
                            {
                                f.Conteudo = "ValorValor" + entry.Value;
                                f.Valor = entry.Value;
                            }
                        }
                        break;
                    case "CDU_password_hash":
                        foreach (var f in info.CamposUtil)
                        {
                            if (f.Nome == "CDU_password_hash")
                            {
                                f.Conteudo = "ValorValor" + entry.Value;
                                f.Valor = entry.Value;
                            }
                        }
                        break;
                    case "CondPag":
                        info.CondPag = entry.Value;
                        break;
                    case "ModoPag":
                        info.ModoPag = entry.Value;
                        break;
                    default:
                        break;
                }
            }

            return WebServicesManager.Instance.WS03_UpdateCustomerAttributes(JsonConvert.SerializeObject(info));
        }
    }
}