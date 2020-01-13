using FullStackJobs.GraphQL.Core.Domain.Entities;


namespace FullStackJobs.GraphQL.Core.Domain.Values
{
    public class Tag : BaseEntity
    {
        public int Id { get; private set; } // EF needs setter
        public Job Job { get; private set; }
        public string Name { get; private set; }

        // required as EF doesn't currently support related entities via constructor: https://github.com/aspnet/EntityFrameworkCore/issues/12078
        internal Tag() { }

        internal Tag(Job job)
        {
            Job = job;
        }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
