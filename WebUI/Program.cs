using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shared.Support.ClassExtensions;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isService = !(Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                string pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            IHostBuilder builder = CreateHostBuilder(args);

            if (isService)
                builder.UseWindowsService();

            IHost host = builder.Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    hostContext.Configuration = ConfigurationsExtensions.ConfigureDefaultJson(hostContext.HostingEnvironment.EnvironmentName).Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://0.0.0.0:5000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
