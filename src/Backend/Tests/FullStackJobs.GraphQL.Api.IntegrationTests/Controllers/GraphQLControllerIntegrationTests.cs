using FullStackJobs.GraphQL.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Testing.Support;
using Xunit;

namespace FullStackJobs.GraphQL.Api.IntegrationTests.Controllers
{
    public class GraphQLControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup, AppDbContext>>
    {
        private readonly HttpClient _client;

        public GraphQLControllerIntegrationTests(CustomWebApplicationFactory<Startup, AppDbContext> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanCreateJobPosting()
        {
            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@" { 'query': 
                                                'mutation($input: CreateJobInput!) {
                                                   createJob(input: $input) { 
                                                     position
                                                   }
                                                }','variables':null}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            /*var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SignupResponse>(stringResponse);
            Assert.Equal(_signupRequests[0].FullName, response.FullName);
            Assert.Equal(_signupRequests[0].Email, response.Email);
            Assert.Equal(_signupRequests[0].Role, response.Role);
            Assert.True(Guid.TryParse(response.Id, out _));*/
        }
    }
}
