using FullStackJobs.GraphQL.Core.Domain.Entities;
using Microsoft.AspNetCore.Builder;
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

            webHostBuilder.Configure(configureApp =>
            {
                using (var serviceScope = configureApp.ApplicationServices.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var dbContext = services.GetService<TDbContext>();                    
                    dbContext.Add(Job.Build("123", "C# Ninja", "logo.png"));
                    dbContext.SaveChanges();
                }

                // The actual Configure() never gets called (unlike ConfigureServices) so we must add routing to the TestServer's pipeline
                configureApp.UseRouting();
                configureApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                });
            });
        }
    }
}
