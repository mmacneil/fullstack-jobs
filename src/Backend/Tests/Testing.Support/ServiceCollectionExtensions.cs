using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace Testing.Support
{
    public static class ServiceCollectionExtensions
    {   
        public static void AddInMemoryDataAccessServices<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            var descriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                        typeof(DbContextOptions<TDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add AppIdentityDbContext using an in-memory database for testing.
            services.AddDbContext<TDbContext>(options =>
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
                var db = scopedServices.GetRequiredService<TDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                // Other setup steps like seeding the database can go here...

                // Seed the database with test data.
                //Utilities.InitializeDbForTests(db);                
            }
        }
    }
}
