using System;
using System.Collections.Generic;
using System.Linq;
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
                Thread.Sleep(1000 * 1000);
            }
        }

        private static void requestFirstToken()
        {
            WS_TokenWithoutCompany request = new WS_TokenWithoutCompany();
            string res = request.Send();
            WebServicesManager.Instance.FirstToken = res;
        }

        private static void requestSecondToken()
        {
            WS_TokenRequest request = new WS_TokenRequest();
            string res = request.Send();
            WebServicesManager.Instance.SecondToken = res;
        }
    }
}
