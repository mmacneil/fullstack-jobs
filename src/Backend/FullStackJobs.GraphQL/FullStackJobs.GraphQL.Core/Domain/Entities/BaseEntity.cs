using System;


namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
