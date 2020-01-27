using FullStackJobs.GraphQL.Infrastructure.Data;
using System.Net.Http;
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
    }
}
