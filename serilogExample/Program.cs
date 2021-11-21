using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace serilogExample
{
    public class Program
    {
        public static void Main(string[] args) =>
         CreateHostBuilder(args)
         .Build()
         .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // no hace falta  - pero puede servir
                // .ConfigureAppConfiguration((context, configuration) =>
                // {
                //     configuration
                //         .SetBasePath(Directory.GetCurrentDirectory())
                //         .AddJsonFile(GetConfigurationFilePath(), optional: false, reloadOnChange: true)
                //         .AddJsonFile(GetConfigurationFilePath($".{context.HostingEnvironment.EnvironmentName}"), optional: true, reloadOnChange: true)
                //         .AddEnvironmentVariables();
                // })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //  private static string GetConfigurationFilePath(string environmentName = null)
        //     => string.Format(CultureInfo.InvariantCulture, "appsettings{0}.json", environmentName);

    }
}
