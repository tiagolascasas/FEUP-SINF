using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBookShop.PrimaveraWebServices
{    public class TokenRequesterThread
    {
        private static bool canRequestFirst = false;
        public static bool okToStart = false;

        public static void DoWork()
        {
            while (true)
            {
                requestSecondToken();
                while (!canRequestFirst){}
                requestFirstToken();
                canRequestFirst = false;
                okToStart = true;
                Thread.Sleep(1080 * 1000);  //refresh every 18 minutes
            }
        }

        private static void requestFirstToken()
        {
            string res;
            do
            {
                Console.Write("Requesting first token...\n");
                res = WebServicesManager.Instance.WS_TokenWithoutCompany();
            } while (res == null || res == "FAIL");

            WebServicesManager.Instance.FirstToken = res;
        }

        private static void requestSecondToken()
        {
            string res;
            do
            {
                Console.Write("Requesting second token...\n");
                res = WebServicesManager.Instance.WS_TokenRequest();
            } while (res == null || res == "FAIL");

            WebServicesManager.Instance.SecondToken = res;

            TokenRequesterThread.canRequestFirst = true;
        }
    }
}
