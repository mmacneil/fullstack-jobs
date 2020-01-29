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
        public async Task CanCreateJob()
        {
            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@" { 'query': 
                                                'mutation($input: CreateJobInput!) {
                                                   createJob(input: $input) {
                                                     id
                                                     position
                                                   }
                                                }','variables':null}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""createJob"":{""id"":2,""position"":""Untitled Position""}}}", content);

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.Last(); // New additions will be at the end of the set behind seeded entries
            Assert.Equal("1", job.EmployerId);
            Assert.Equal("Untitled Position", job.Position);
        }

        [Fact]
        public async Task CanFetchJob()
        {
            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@"{""query"":""query FullStackJobsQuery($id: Int!)
                                                {
                                                    job(id: $id) {
                                                        position   
                                                    }
                                                }"",
                                                ""variables"":{""id"":1},
                                                ""operationName"":""FullStackJobsQuery""}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""job"":{""position"":""C# Ninja""}}}", content);

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.First();
            Assert.Equal("123", job.EmployerId);
            Assert.Equal("C# Ninja", job.Position);
        }
    }
}


