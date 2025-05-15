using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.ConsoleApp
{
    public class AppSettingsHandler
    {
        private string _filename;
        private AppSettings _config;
        public AppSettingsHandler(string filename)
        {
            _filename = filename;
            _config = GetAppSettings();
        }
        public AppSettings GetAppSettings()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile(_filename, false, true)
               .Build();
            
            var appSettings = new AppSettings();
            config.GetSection("App").Bind(appSettings);

            return appSettings;
        }
    }
}
