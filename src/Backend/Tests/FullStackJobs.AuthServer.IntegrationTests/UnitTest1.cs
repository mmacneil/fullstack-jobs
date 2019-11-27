using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace FullStackJobs.AuthServer.IntegrationTests
{
    public class PlayersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PlayersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetPlayers()
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/accounts");

            request.Content = new StringContent("{\"fullName\":\"John Doe\",\"email\":33}",
                Encoding.UTF8,
                "application/json");//CONTENT-TYPE header

            //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            // The endpoint or route of the controller action.
            var httpResponse = await _client.SendAsync(request);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            //  var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            // var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(stringResponse);
            // Assert.Contains(players, p => p.FirstName == "Wayne");
            //  Assert.Contains(players, p => p.FirstName == "Mario");
        }
    }
}
