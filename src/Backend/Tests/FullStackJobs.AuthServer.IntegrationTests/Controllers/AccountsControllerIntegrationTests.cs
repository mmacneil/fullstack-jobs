using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FullStackJobs.AuthServer.Models;
using Newtonsoft.Json;
using Xunit;

namespace FullStackJobs.AuthServer.IntegrationTests.Controllers
{
    public class AccountsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
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
    }
}
