using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testing.Support;
using Testing.Support.UserFilters;
using Xunit;

namespace FullStackJobs.GraphQL.Api.IntegrationTests
{
    // https://gunnarpeipman.com/aspnet-core-identity-integration-tests/
    public class IntegrationTestBase<TDbContext, TStartup> : IClassFixture<FullStackJobsApplicationFactory<TStartup>> where TDbContext : DbContext where TStartup : class
    {
        private readonly WebApplicationFactory<TStartup> _factory;

        public IntegrationTestBase(FullStackJobsApplicationFactory<TStartup> factory)
        {
            _factory = factory;
        }

        protected WebApplicationFactory<TStartup> GetFactory(bool isEmployer = false, bool isApplicant = false)
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddInMemoryDataAccessServices<TDbContext>();
                });

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(options =>
                    {
                        if (isEmployer)
                        {
                            options.Filters.Add(new EmployerUserFilter());
                        }
                        if (isApplicant)
                        {
                            options.Filters.Add(new ApplicantUserFilter());
                        }
                    });
                });
            });
        }
    }
}
