using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Shared.Support.ClassExtensions
{
    public static class ConfigurationsExtensions
    {
        public static IConfigurationBuilder ConfigureDefaultJson(string environment)
        {
            var sharedFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\Shared"));
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{environment}.json"), optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
        }
    }
}
