using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvcBookShop.PrimaveraWebServices;

namespace MvcBookShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread tokenRequester = new Thread(TokenRequesterThread.DoWork);
            tokenRequester.Start();

            while (!TokenRequesterThread.okToStart) { }
            Console.Write("Tokens ok, starting in 3 seconds...");
            System.Threading.Thread.Sleep(3000);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
