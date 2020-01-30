using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testing.Support;

namespace FullStackJobs.GraphQL.Api.IntegrationTests.Fixtures
{
    public class GraphQLApiWebApplicationFactory<TDbContext> : WebApplicationFactory<Startup> where TDbContext : DbContext
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddInMemoryDataAccessServices<TDbContext>();
            });

            webHostBuilder.ConfigureTestServices(services =>
            {
                services.AddMvc(options =>
                {
                    options.Filters.Add(new FakeUserFilter());
                });
            });
        }
    }
}
