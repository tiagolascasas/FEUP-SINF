using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MvcBookShop.PrimaveraWebServices
{
    class WS_TokenRequest : WS
    {
        public WS_TokenRequest()
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
                { "grant_type", WebServicesManager.Instance.GrandType },
                { "line", WebServicesManager.Instance.Line }
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