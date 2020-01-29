using System;
using Microsoft.EntityFrameworkCore;

namespace Testing.Support
{
    public static class DbContextFactory
    {
        public static TDbContext MakeInMemoryProviderDbContext<TDbContext>(string databaseName) where TDbContext : DbContext
        {
            return Activator.CreateInstance(typeof(TDbContext), new DbContextOptionsBuilder<TDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options) as TDbContext;
        }
    }
}

