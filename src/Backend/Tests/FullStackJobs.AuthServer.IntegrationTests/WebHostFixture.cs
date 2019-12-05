using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace FullStackJobs.AuthServer.IntegrationTests
{
    public sealed class WebHostFixture : IDisposable
    {
        private readonly IWebHost _webHost;
        public WebHostFixture()
        {
            _webHost = WebHost.CreateDefaultBuilder()
                  .UseStartup<FakeStartup>()
                  .UseKestrel()
                  .UseUrls(Constants.HostAddress)
                  //todo: resolve this 
                  .UseContentRoot(@"C:\code\fullstack-jobs\src\Backend\FullStackJobs.AuthServer\FullStackJobs.AuthServer")
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
