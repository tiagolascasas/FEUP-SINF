using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcBookShop.PrimaveraWebServices
{
    public class WS_TokenWithoutCompany : WS
    {
        public WS_TokenWithoutCompany()
        {
            this.route = WebServicesManager.Instance.ApiUrl + "token";
        }

        public override string Send()
        {
            var body = new Dictionary<string, string>
            {
                { "username", WebServicesManager.Instance.Username },
                { "password", WebServicesManager.Instance.Password },
                { "instance", WebServicesManager.Instance.InstanceAPI },
                { "grant_type", WebServicesManager.Instance.GrandType }
            };

            HttpContent content = new FormUrlEncodedContent(body);

            HttpResponseMessage response = WebServicesManager.Client.PostAsync(this.route, content).Result;

            string res = response.Content.ReadAsStringAsync().Result;

            dynamic parsed = JObject.Parse(res);
            string token = parsed.access_token;

            return token;
        }
    }
}
