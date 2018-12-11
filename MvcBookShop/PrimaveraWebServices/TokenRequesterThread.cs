using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBookShop.PrimaveraWebServices
{    public class TokenRequesterThread
    {
        public static void DoWork()
        {
            while (true)
            {
                requestSecondToken();
                requestFirstToken();
                Thread.Sleep(6000);
                //test calls here
                //bool res = WebServicesManager.Instance.WS01_CreateCustomer("{\n    \"Cliente\": \"TIAGO13\",\n    \"Nome\": \"Tiago Santos\",\n    \"Morada\": \"Rua Eng. Farinas de Almeida, 313, 4º esq\",\n    \"CodigoPostal\": \"4510-260\",\n    \"Telefone\": \"961843943\",\n    \"NumContribuinte\": \"64287048860\",\n    \"Pais\": \"PT\",\n    \"Moeda\": \"EUR\"\n}");

                Thread.Sleep(1000 * 1000);
            }
        }

        private static void requestFirstToken()
        {
            string res = WebServicesManager.Instance.WS_TokenWithoutCompany();
            WebServicesManager.Instance.FirstToken = res;
        }

        private static void requestSecondToken()
        {
            string res = WebServicesManager.Instance.WS_TokenRequest();
            WebServicesManager.Instance.SecondToken = res;
        }
    }
}
