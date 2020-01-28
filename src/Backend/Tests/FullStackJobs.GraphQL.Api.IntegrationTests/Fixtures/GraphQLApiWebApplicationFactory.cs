using FullStackJobs.GraphQL.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testing.Support;

namespace FullStackJobs.GraphQL.Api.IntegrationTests.Fixtures
{
    public class GraphQLApiWebApplicationFactory<TDbContext> : WebApplicationFactory<Startup> where TDbContext : DbContext
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddInMemoryDataAccessServices<TDbContext>();
            });

            builder.Configure(builder =>
            {
                using (var serviceScope = builder.ApplicationServices.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var dbContext = services.GetService<TDbContext>();

                    // INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName]) VALUES (N'2fd44c75-4c08-4723-82dc-efd21d578627', N'testuser@fullstackjobs.com', N'TESTUSER@FULLSTACKJOBS.COM', N'testuser@fullstackjobs.com', N'TESTUSER@FULLSTACKJOBS.COM', 0, N'AQAAAAEAACcQAAAAELDzmzW/2zZATdC1rhDbDBEHWYziIPV6U2mOtAXfKFUgIg1vtPXlgOetL8tBPKuSrg==', N'4ZGZFFTIFBZVLFDGY7ZQ4O74R3ZOCCJM', N'81ba8fc8-6db7-4ae4-9574-4729473c60c2', NULL, 0, 0, NULL, 1, 0, N'Test User')
                    var count = dbContext.Database.ExecuteSqlRaw("INSERT AspNetUsers ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName]) VALUES (N'2fd44c75-4c08-4723-82dc-efd21d578627', N'testuser@fullstackjobs.com', N'TESTUSER@FULLSTACKJOBS.COM', N'testuser@fullstackjobs.com', N'TESTUSER@FULLSTACKJOBS.COM', 0, N'AQAAAAEAACcQAAAAELDzmzW/2zZATdC1rhDbDBEHWYziIPV6U2mOtAXfKFUgIg1vtPXlgOetL8tBPKuSrg==', N'4ZGZFFTIFBZVLFDGY7ZQ4O74R3ZOCCJM', N'81ba8fc8-6db7-4ae4-9574-4729473c60c2', NULL, 0, 0, NULL, 1, 0, N'Test User')");
                }
            });
        }
    }
}
