using FullStackJobs.AuthServer.IntegrationTests.Shared;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace FullStackJobs.AuthServer.IntegrationTests.Fixtures
{
    public sealed class WebHostFixture : IDisposable
    {
        private readonly IWebHost _webHost;
        public WebHostFixture()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            _webHost = WebHost.CreateDefaultBuilder()
                  .UseStartup<FakeStartup>()
                  .UseKestrel()
                  .UseUrls(Constants.HostAddress)
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
