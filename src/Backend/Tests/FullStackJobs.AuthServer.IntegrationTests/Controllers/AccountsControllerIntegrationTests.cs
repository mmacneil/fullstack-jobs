using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FullStackJobs.AuthServer.Models;
using FullStackJobs.AuthServer.Models.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PuppeteerSharp;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
using Xunit;

namespace FullStackJobs.AuthServer.IntegrationTests.Controllers
{
    public class AccountsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        //private readonly IWebDriver _driver;
        private readonly HttpClient _client;

        public AccountsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            // _driver = new ChromeDriver();
        }

        [Fact]
        public async Task CanCreateAccount()
        {
            const string fullName = "Mark Macneil", email = "mark@fullstackmark.com", role = "applicant";

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/accounts")
            {
                Content = new StringContent($"{{\"fullName\":\"{fullName}\",\"email\":\"{email}\",\"password\":\"Pa$$w0rd!\",\"role\":\"{role}\"}}", Encoding.UTF8, "application/json")
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SignupResponse>(stringResponse);
            Assert.Equal(fullName, response.FullName);
            Assert.Equal(email, response.Email);
            Assert.Equal(role, response.Role);
            Assert.True(Guid.TryParse(response.Id, out _));
        }

        [Fact]
        public async Task CanLogin()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                     .UseStartup<FakeStartup>()
                     .UseKestrel()
                     .UseUrls("http://localhost:5556")
                     .UseContentRoot(@"C:\code\fullstack-jobs\src\Backend\FullStackJobs.AuthServer\FullStackJobs.AuthServer")
                     .UseEnvironment("Development")
                     .Build();

            
            webHost.Start();

            const string fullName = "Mark Macneil", email = "mark@fullstackmark.com", role = "applicant";
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/accounts")
            {
                Content = new StringContent($"{{\"fullName\":\"{fullName}\",\"email\":\"{email}\",\"password\":\"Pa$$w0rd!\",\"role\":\"{role}\"}}", Encoding.UTF8, "application/json")
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    page.Console += Page_Console;

                    await page.GoToAsync("http://localhost:5556/test-client/index.html");

                    var navigationTask = page.WaitForNavigationAsync();
                    await Task.WhenAll(
                           navigationTask,
                           page.ClickAsync("button"));

                    await page.TypeAsync("#Username", "mark@fullstackmark.com");
                    await page.TypeAsync("#Password", "Pa$$w0rd!");
                    await page.ClickAsync(".btn-primary");

                    var content = await page.GetContentAsync();
                    bool here = true;
                }
            };        
        }

        private void Page_Console(object sender, ConsoleEventArgs e)
        {
            var message = e.Message;
        }
    }
}
