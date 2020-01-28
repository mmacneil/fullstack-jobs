using System.Linq;
using FullStackJobs.GraphQL.Api.IntegrationTests.Fixtures;
using FullStackJobs.GraphQL.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Testing.Support;
using Xunit;

namespace FullStackJobs.GraphQL.Api.IntegrationTests.Controllers
{
    public class GraphQLControllerIntegrationTests : IClassFixture<GraphQLApiWebApplicationFactory<AppDbContext>>
    {
        private readonly HttpClient _client;

        public GraphQLControllerIntegrationTests(GraphQLApiWebApplicationFactory<AppDbContext> factory)
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

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.First();
            Assert.Equal("1", job.EmployerId);
            Assert.Equal("Untitled Position", job.Position);
        }
    }
}
