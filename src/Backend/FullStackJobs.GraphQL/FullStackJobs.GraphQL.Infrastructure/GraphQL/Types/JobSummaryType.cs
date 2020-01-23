using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Core.Domain.Values;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;
using GraphQL.Types;

namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types
{
    public class JobSummaryType : ObjectGraphType<Job>
    {
        public JobSummaryType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id);
            Field(x => x.Position);
            Field(x => x.Company, true);
            Field(x => x.Icon);
            Field(x => x.Location, true);
            Field<EnumerationGraphType<Status>>("status");
            Field<StringGraphType>("modified", resolve: context => contextServiceLocator.Humanizer.TimeAgo(context.Source.Modified ?? context.Source.Created));
            Field<IntGraphType>("applicantCount", resolve: context => context.Source.JobApplicants.Count);
        }
    }
}
