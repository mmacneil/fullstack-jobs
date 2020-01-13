

namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public class Employer : User
    {
        internal Employer(string id, string fullName) : base(id, fullName)
        {
        }
    }
}
