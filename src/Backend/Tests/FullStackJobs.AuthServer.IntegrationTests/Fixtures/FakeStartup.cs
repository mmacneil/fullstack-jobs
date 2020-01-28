using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testing.Support;

namespace FullStackJobs.AuthServer.IntegrationTests.Fixtures
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void AddDbContext(IServiceCollection services)
        {
            base.AddDbContext(services);
            services.AddInMemoryDataAccessServices<AppIdentityDbContext>();
        }

        protected override void ConfigureDatabase(DbContextOptionsBuilder ctxBuilder)
        {
            ctxBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
        }
    }
}
