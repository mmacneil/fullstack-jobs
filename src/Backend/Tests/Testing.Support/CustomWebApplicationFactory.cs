using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Testing.Support
{
    public class CustomWebApplicationFactory<TStartup, TDbContext> : WebApplicationFactory<TStartup> where TStartup : class where TDbContext : DbContext
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                          .UseStartup<TStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddInMemoryDataAccessServices<TDbContext>();
            });
        }
    }
}
