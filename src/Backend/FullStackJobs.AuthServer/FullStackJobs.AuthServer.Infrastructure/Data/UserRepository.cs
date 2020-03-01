using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FullStackJobs.AuthServer.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public UserRepository(AppIdentityDbContext appIdentityDbContext)
        {
            _appIdentityDbContext = appIdentityDbContext;
        }

        public async Task InsertEntity(string role, string userId, string fullName)
        {
            // Insert in entity table
            var commandText = $"INSERT {role + "s"} (Id,Created,FullName) VALUES (@Id,getutcdate(),@FullName)";
            var id = new SqlParameter("@Id", userId);
            var name = new SqlParameter("@FullName", fullName);
            await _appIdentityDbContext.Database.ExecuteSqlRawAsync(commandText, id, name);
        }
    }
}
