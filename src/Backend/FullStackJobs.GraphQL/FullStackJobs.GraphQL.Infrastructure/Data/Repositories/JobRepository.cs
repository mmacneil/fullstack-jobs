using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Core.Interfaces.Gateways.Repositories;


namespace FullStackJobs.GraphQL.Infrastructure.Data.Repositories
{
    public sealed class JobRepository : EfRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
