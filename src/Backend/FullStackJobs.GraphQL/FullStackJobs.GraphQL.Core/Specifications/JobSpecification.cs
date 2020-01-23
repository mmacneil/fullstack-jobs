using FullStackJobs.GraphQL.Core.Domain.Entities;
using System;
using System.Linq.Expressions;


namespace FullStackJobs.GraphQL.Core.Specifications
{
    public sealed class JobSpecification : BaseSpecification<Job>
    {
        public JobSpecification(Expression<Func<Job, bool>> criteria) : base(criteria)
        {
            AddInclude(j => j.Employer);
            AddInclude(j => j.Tags);
            AddInclude(j => j.JobApplicants);
        }
    }
}
