using Agricathon_gr3.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new VSContext();
            var e = context.Database.EnsureCreated();

            if (e)
            {
                Console.Write("DB created");
            }
            else
            {
                Console.Write("DB already exists");

            }
            Console.Write("Done");
            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
