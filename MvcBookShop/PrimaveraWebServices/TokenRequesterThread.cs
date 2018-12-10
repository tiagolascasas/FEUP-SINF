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
