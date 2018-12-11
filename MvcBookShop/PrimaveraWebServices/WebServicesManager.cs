using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace MvcBookShop.PrimaveraWebServices
{
    public sealed class WebServicesManager
    {
        private static readonly WebServicesManager instance = new WebServicesManager();
        private static readonly HttpClient client = new HttpClient();

        private string apiUrl = "http://localhost:8001/WebApi/";
        private string username = "FEUP";
        private string password = "qualquer1";
        private string company = "BOOKSHOP";
        private string instanceAPI = "Default";
        private string grandType = "password";
        private string secondToken = "";
        private string line = "professional";
        private string firstToken = "";

        static WebServicesManager()
        {
        }

        private WebServicesManager()
        {
        }

        public static WebServicesManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static HttpClient Client => client;

        public string FirstToken { get => firstToken; set => firstToken = value; }
        public string SecondToken { get => secondToken; set => secondToken = value; }
        public string ApiUrl { get => apiUrl; set => apiUrl = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Company { get => company; set => company = value; }
        public string InstanceAPI { get => instanceAPI; set => instanceAPI = value; }
        public string GrandType { get => grandType; set => grandType = value; }
        public string Line { get => line; set => line = value; }

        public string WS_TokenWithoutCompany()
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "token");

            client.ClearHandlers();
            var jsonDeserializer = new JsonDeserializer();
            client.AddHandler("application/json", jsonDeserializer);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "ea40762a-c25a-4cec-b2ae-1474376fc6ee");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "username=FEUP&password=qualquer1&instance=Default&grant_type=password", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                dynamic data = JObject.Parse(rawResponse);
                Console.WriteLine("Second token: " + data.access_token);
                return data.access_token;
            }
            else
                return "FAIL";
        }

        public string WS_TokenRequest()
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "token");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "2207eb42-0c9d-4489-85d2-ff8093a9684a");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "username=FEUP&password=qualquer1&company=BOOKSHOP&instance=Default&grant_type=password&line=professional", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                dynamic data = JObject.Parse(rawResponse);
                Console.WriteLine("First token: " + data.access_token);
                return data.access_token;
            }
            else
                return "FAIL";
        }

        public bool WS01_CreateCustomer(string json)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Base/Clientes/Actualiza");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "33c5105d-98c5-470b-a853-d23f4a7661da");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write("Status code: " + response.StatusCode);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public string WS02_GetCustomerInformation(string Cliente)
        {
            string route = WebServicesManager.Instance.ApiUrl + "Base/Clientes/Edita/" + Cliente;



            return "";
        }

        public bool WS03_UpdateCustomerAttributes(string json)
        {
            return true;
        }

        public string WS04_GetBookInformation(string Artigo)
        {
            return "";
        }

        public string WS05_GetSetOfBooksInCategory(string Categoria)
        {
            return "";
        }

        public string WS06_SearchByTitle(string Titulo)
        {
            return "";
        }

        public string WS07_GetSetOfBooksOrderedByStock()
        {
            return "";
        }

        public string WS08_GetSetOfBooksOrderedByRelease()
        {
            return "";
        }

        public bool WS09_PlaceOrder(string document)
        {
            return true;
        }

        public bool WS10_PurchaseItem(string document)
        {
            return true;
        }

        public string WS11_GetDocument(int id)
        {
            return "";
        }
    }
}