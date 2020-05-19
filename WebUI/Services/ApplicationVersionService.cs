using Application.Common.Interfaces;
using System;
using System.IO;

namespace WebUI.Services
{
    public class ApplicationVersionService : IApplicationVersionService
    {
        public int AppVersion => GetAppVersion();

        private int GetAppVersion()
        {
            try
            {
                var dataFile = AppDomain.CurrentDomain.BaseDirectory + "version.info";
                string content = File.ReadAllText(dataFile);
                string[] version = content.Split('.');

                return int.Parse(version[0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
