
using System;
using System.Text;
using System.Data;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

using MvcBookShop.Models;
using MvcBookShop.PrimaveraWebServices;
using MvcBookShop.Utility;

namespace MvcBookShop.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            try
            {
                dynamic json = WebServicesManager.Instance.WS02_GetCustomerInformation(Request.Form["username"]);


                SHA256 sha256Hash = SHA256.Create();
                Console.WriteLine("\n\n\n\n\n\n\n");

                if (VerifyHash(sha256Hash, Request.Form["password"], (string)json.CamposUtil[4].Valor))
                {
                    Console.WriteLine("\nHASHES ARE EQUAL");

                    string postalCode = Convert.ToString(json.CodigoPostal);
                    string nIF = Convert.ToString(json.NumContribuinte);
                    string phone = Convert.ToString(json.Telefone);
                    HttpContext.Session.SetString("username", Request.Form["username"]);
                    HttpContext.Session.SetString("Nome", (string)json.Nome);
                    HttpContext.Session.SetString("Morada", (string)json.Morada);
                    HttpContext.Session.SetString("CodigoPostal", postalCode);
                    HttpContext.Session.SetString("NIF", nIF);
                    HttpContext.Session.SetString("Telefone", phone);

                }
                else
                    return BadRequest("Incorrect Password");
                Console.WriteLine("\n\n\n\n\n\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return BadRequest("INVALID USERNAME");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("username") == null)
                return BadRequest("You're not logged in");

            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Remove("Morada");
            HttpContext.Session.Remove("CodigoPostal");
            HttpContext.Session.Remove("NIF");
            HttpContext.Session.Remove("Telefone");

            return RedirectToAction("Index", "Home");
        }

        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
