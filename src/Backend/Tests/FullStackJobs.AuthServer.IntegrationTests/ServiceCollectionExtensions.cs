using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace FullStackJobs.AuthServer.IntegrationTests
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryDataAccessServices(this IServiceCollection services)
        {
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
