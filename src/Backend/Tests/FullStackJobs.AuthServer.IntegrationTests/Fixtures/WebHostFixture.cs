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
                    _host = Helpers.GetIConfigurationRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).GetValue("AppSettings:Address", "");
                }
                return _host;
            }
        }
        public WebHostFixture()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            _webHost = WebHost.CreateDefaultBuilder()
                  .UseStartup<TestStartup>()
                  .UseKestrel((host, options) =>
                 {
                     options.Listen(IPEndPoint.Parse(Host));

                 })
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
