using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackJobs.AuthServer.IntegrationTests
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

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            base.ConfigureDatabase(services);
            services.AddInMemoryDataAccessServices();
        }
    }
}
