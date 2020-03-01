using FullStackJobs.AuthServer.Infrastructure.Data;
using System.Threading.Tasks;

namespace FullStackJobs.AuthServer.IntegrationTests.Fixtures
{
    public class MockUserRepository : IUserRepository
    {
        public Task InsertEntity(string role, string id, string fullName)
        {
            return Task.CompletedTask;
        }
    }
}
