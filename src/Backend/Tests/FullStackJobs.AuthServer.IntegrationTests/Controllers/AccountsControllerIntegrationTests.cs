using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FullStackJobs.AuthServer.Models;
using Newtonsoft.Json;
using PuppeteerSharp;
using Xunit;

namespace FullStackJobs.AuthServer.IntegrationTests.Controllers
{
    [Collection("WebHost collection")]
    public class AccountsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string _fullName = "Mark Macneil", _email = "mark@fullstackmark.com", _password = "Pa$$w0rd!", _role = "applicant";

        private readonly HttpRequestMessage _createAccountRequest = new HttpRequestMessage(HttpMethod.Post, "/api/accounts")
        {
            Content = new StringContent($"{{\"fullName\":\"{_fullName}\",\"email\":\"{_email}\",\"password\":\"{_password}\",\"role\":\"{_role}\"}}", Encoding.UTF8, "application/json")
        };

        private readonly HttpClient _client;

        public AccountsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanCreateAccount()
        {
            var httpResponse = await _client.SendAsync(_createAccountRequest);

            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SignupResponse>(stringResponse);
            Assert.Equal(_fullName, response.FullName);
            Assert.Equal(_email, response.Email);
            Assert.Equal(_role, response.Role);
            Assert.True(Guid.TryParse(response.Id, out _));
        }

        [Fact]
        public async Task CanLogin()
        {
            var httpResponse = await _client.SendAsync(_createAccountRequest);

            httpResponse.EnsureSuccessStatusCode();

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync($"{Constants.HostAddress}/test-client/index.html");

                    var navigationTask = page.WaitForNavigationAsync();

                    await Task.WhenAll(navigationTask, page.ClickAsync("button"));

                    await page.TypeAsync("#Username", _email);
                    await page.TypeAsync("#Password", _password);

                    navigationTask = page.WaitForNavigationAsync(new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } });
                    await Task.WhenAll(navigationTask, page.ClickAsync(".btn-primary"));

                    var content = await page.GetContentAsync();
                    await page.CloseAsync();

                    Assert.Contains("User logged in", content);
                }
            };
        }
    }
}
