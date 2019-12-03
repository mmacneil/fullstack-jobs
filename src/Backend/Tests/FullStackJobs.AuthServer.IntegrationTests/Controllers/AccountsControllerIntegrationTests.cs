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

            var timeout = TimeSpan.FromSeconds(3).Milliseconds;

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync("http://localhost:5556/oidc-client.html");
                    await page.GetContentAsync();
                    await Task.WhenAll(page.ClickAsync("#login"), page.WaitForNavigationAsync());
                    var content = await page.GetContentAsync();
                }
            };




            //  await page.GoToAsync("http://localhost:5556/oidc-client.html");
            // var content = await page.GetContentAsync();
            // await page.ClickAsync("#button");

            // var title = await page.GetTitleAsync();


            /*var page = await browser.NewPageAsync();
            await page.GoToAsync("http://localhost:5556/accounts/login");


            await page.TypeAsync("#Username", "mark@fullstackmark.com");
            await page.TypeAsync("#Password", "Pa$$w0rd!");

            await page.ClickAsync(".btn-primary");

            await page.WaitForNavigationAsync();

            var content = await page.GetContentAsync();*/





            //%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3Dangular_spa%26redirect_uri%3Dhttp%253A%252F%252Flocalhost%253A4200%252Fauth-callback%26response_type%3Dcode%26scope%3Dopenid%2520profile%2520email%2520api.read%26state%3D48620baa7c274e6b958e2bd4b7ae7c82%26code_challenge%3D44oYGNSdqpc1EG229mHg8EXZSJQNPF1z2CF4U0J75is%26code_challenge_method%3DS256%26response_mode%3Dquery%26newAccount%3Dtrue%26userName%3Dmigudu@mailinator.com

            /*
            const string fullName = "Mark Macneil", email = "mark@fullstackmark.com", role = "applicant";

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/accounts")
            {
                Content = new StringContent($"{{\"fullName\":\"{fullName}\",\"email\":\"{email}\",\"password\":\"Pa$$w0rd!\",\"role\":\"{role}\"}}", Encoding.UTF8, "application/json")
            };

            // The endpoint or route of the controller action.
            var httpResponse = await _client.SendAsync(request);

            var model = new LoginInputModel
            {
                Username = "mark@fullstackmark.com",
                Password = "Pa$$w0rd!"
            };*/
        }
    }
}
