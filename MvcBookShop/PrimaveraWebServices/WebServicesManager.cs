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

        public JObject WS02_GetCustomerInformation(string Cliente)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Base/Clientes/Edita/" + Cliente);
            var request = new RestRequest(Method.GET);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "dc08a379-325d-4ae6-95a0-b96e7e6645ce");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public bool WS03_UpdateCustomerAttributes(string json)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Base/Clientes/Actualiza");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "435849cc-205b-4522-8cd4-68a6bf9980fe");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write("Status code: " + response.StatusCode);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public string WS04_GetBookInformation(string Artigo)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Administrador/Consulta");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "27416828-1a92-4ea2-9cc8-b4b8c62b38b4");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            string sql = string.Format("\"Select Descricao, CDU_Autor, CDU_Sinopse, CDU_ISBN, CDU_Editora, CDU_Capa, CDU_Paginas, CDU_Ano, CDU_Dimensoes, Familia, CDU_Idioma, PVP1 from Artigo, ArtigoMoeda where Artigo.Artigo = '{0}' and Artigo.Artigo = ArtigoMoeda.Artigo\"", Artigo);
            Console.Write(sql);
            request.AddParameter("undefined", sql, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public dynamic WS05_GetSetOfBooksInCategory(string Categoria)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Administrador/Consulta");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "6d49e4f9-f603-4583-bfeb-d75f47f8fcab");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            string sql = string.Format("\"Select Artigo.Artigo, Descricao, PVP1, CDU_Autor from Artigo, ArtigoMoeda where Familia = '{0}' and Artigo.Artigo = ArtigoMoeda.Artigo\"", Categoria);
            request.AddParameter("undefined", sql, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public dynamic WS06_SearchByTitle(string Titulo)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Administrador/Consulta");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "2c465850-c3f3-4853-adce-e1abc62d5959");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            string sql = string.Format("\"Select Artigo.Artigo, Descricao, CDU_Autor, PVP1 from Artigo, ArtigoMoeda where Descricao like '%{0}%' and Artigo.Artigo = ArtigoMoeda.Artigo\"", Titulo);
            request.AddParameter("undefined", sql, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public dynamic WS07_GetSetOfBooksOrderedByStock()
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Administrador/Consulta");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "e0a43d1a-b683-4224-b212-022e7b33a849");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "\"Select Artigo.Artigo, Descricao, PVP1, CDU_Autor, STKActual from Artigo, ArtigoMoeda where Artigo.Artigo = ArtigoMoeda.Artigo and STKActual > 0 order by STKActual\"", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public dynamic WS08_GetSetOfBooksOrderedByRelease()
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Administrador/Consulta");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "4538e85d-c884-4bd3-9bd7-527b3eab6cd1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "\"Select Artigo.Artigo, Descricao, PVP1, CDU_Autor, DataUltimaActualizacao from Artigo, ArtigoMoeda where Artigo.Artigo = ArtigoMoeda.Artigo order by DataUltimaActualizacao desc\"", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }

        public bool WS09_PlaceOrder(string Document)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Vendas/Docs/CreateDocument");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "34ce0281-6761-4329-949e-0027f77b0527");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", Document, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write("Status code: " + response.StatusCode);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool WS10_PurchaseItem(string Document)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Vendas/Docs/CreateDocument");
            var request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "34ce0281-6761-4329-949e-0027f77b0527");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", Document, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.Write("Status code: " + response.StatusCode);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public dynamic WS11_GetDocument(int Id)
        {
            var client = new RestClient(WebServicesManager.Instance.ApiUrl + "Vendas/Docs/Edita/000/ECL/A/" + Id);
            var request = new RestRequest(Method.GET);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", WebServicesManager.Instance.SecondToken));
            request.AddHeader("Postman-Token", "f8e89300-5bf7-49dd-a660-d3a223910186");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);

            Console.Write(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                Console.Write(rawResponse);
                dynamic data = JObject.Parse(rawResponse);
                return data;
            }
            else
                return null;
        }
    }
}