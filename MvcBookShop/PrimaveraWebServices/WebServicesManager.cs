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
    } 
}