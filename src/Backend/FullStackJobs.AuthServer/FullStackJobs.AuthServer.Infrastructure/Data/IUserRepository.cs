using System.Threading.Tasks;

namespace FullStackJobs.AuthServer.Infrastructure.Data
{
    public interface IUserRepository
    {
        Task InsertEntity(string role, string id, string fullName);
    }
}
