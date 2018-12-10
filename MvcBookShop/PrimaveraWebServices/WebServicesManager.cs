using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
            string route = WebServicesManager.Instance.ApiUrl + "token";

            var body = new Dictionary<string, string>
            {
                { "username", WebServicesManager.Instance.Username },
                { "password", WebServicesManager.Instance.Password },
                { "instance", WebServicesManager.Instance.InstanceAPI },
                { "grant_type", WebServicesManager.Instance.GrandType }
            };

            HttpContent content = new FormUrlEncodedContent(body);

            HttpResponseMessage response = WebServicesManager.Client.PostAsync(route, content).Result;

            string res = response.Content.ReadAsStringAsync().Result;

            dynamic parsed = JObject.Parse(res);
            string token = parsed.access_token;

            return token;
        }

        public string WS_TokenRequest()
        {
            string route = WebServicesManager.Instance.ApiUrl + "token";

            var body = new Dictionary<string, string>
            {
                { "username", WebServicesManager.Instance.Username },
                { "password", WebServicesManager.Instance.Password },
                { "instance", WebServicesManager.Instance.InstanceAPI },
                { "grant_type", WebServicesManager.Instance.GrandType },
                { "line", WebServicesManager.Instance.Line }
            };

            HttpContent content = new FormUrlEncodedContent(body);

            HttpResponseMessage response = WebServicesManager.Client.PostAsync(route, content).Result;

            string res = response.Content.ReadAsStringAsync().Result;

            dynamic parsed = JObject.Parse(res);
            string token = parsed.access_token;

            return token;
        }

        public bool WS01_CreateCustomer(string json)
        {
            return true;
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