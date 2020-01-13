

namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Id { get; private set; }
        public string FullName { get; private set; }

        internal User(string id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}
