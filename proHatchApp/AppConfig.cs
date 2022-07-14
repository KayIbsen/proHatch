using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace proHatchApp
{
    public class AppConfig
    {
        private readonly IConfigurationRoot _configurationRoot;

        public AppConfig()
        {


        var currentDirectory = Environment.CurrentDirectory;

        var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: false);

            _configurationRoot = builder.Build();
        }
        public T GetSection<T>(string key) => _configurationRoot.GetSection(key).Get<T>();
    }
}
