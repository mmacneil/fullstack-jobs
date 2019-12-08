using FullStackJobs.AuthServer.IntegrationTests.Shared;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace FullStackJobs.AuthServer.IntegrationTests.Fixtures
{

    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }

    public sealed class WebHostFixture : IDisposable
    {
        private readonly IWebHost _webHost;

        private string _host;
        public string Host
        {
            get
            {
                if (string.IsNullOrEmpty(_host))
                {
                    var config = TestHelper.GetIConfigurationRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                    _host = config.GetValue("AppSettings:Address", "");
                }
                return _host;
            }
        }
        public WebHostFixture()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly; 

            _webHost = WebHost.CreateDefaultBuilder()
                  .UseStartup<FakeStartup>()
                  .UseKestrel((host, options) =>
                 {
                     options.Listen(IPEndPoint.Parse(Host));
                 })
                  //.UseUrls(Constants.HostAddress)
                  .UseContentRoot(Path.Combine(Helpers.GetProjectPath(startupAssembly), startupAssembly.GetName().Name))
                  .UseEnvironment("Development")
                  .Build();

            _webHost.Start();
        }

        public void Dispose()
        {
            _webHost.Dispose();
        }
    }
}
