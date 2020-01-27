using FullStackJobs.GraphQL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace FullStackJobs.GraphQL.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
