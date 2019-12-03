using System.Linq;
using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

            var descriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                        typeof(DbContextOptions<AppIdentityDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add AppIdentityDbContext using an in-memory database for testing.
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppIdentityDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                // Other setup steps like seeding the database can go here...

                // Seed the database with test data.
                //Utilities.InitializeDbForTests(db);                
            }

        }
    }
}
