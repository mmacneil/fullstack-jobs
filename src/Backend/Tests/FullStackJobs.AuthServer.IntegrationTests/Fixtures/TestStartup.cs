using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testing.Support;

namespace FullStackJobs.AuthServer.IntegrationTests.Fixtures
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void AddDbContext(IServiceCollection services)
        {
            base.AddDbContext(services);
            services.AddInMemoryDataAccessServices<AppIdentityDbContext>();
        }

        protected override void ConfigureDatabase(DbContextOptionsBuilder ctxBuilder)
        {
            ctxBuilder.UseInMemoryDatabase();
        }
    }
}
