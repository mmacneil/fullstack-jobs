using FullStackJobs.GraphQL.Common;
using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Core.Domain.Values;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;
using GraphQL.Authorization;
using GraphQL.Types;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types
{
    public class JobType : ObjectGraphType<Job>
    {
        public JobType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id);
            Field(x => x.Position);
            Field(x => x.Company, true);
            Field(x => x.Icon);
            Field(x => x.Location, true);
            Field(x => x.AnnualBaseSalary, true).AuthorizeWith(Policies.Employer);
            Field(x => x.Description, true);
            Field(x => x.Responsibilities, true);
            Field(x => x.Requirements, true);
            Field(x => x.ApplicationInstructions, true);
            Field<ListGraphType<TagType>>("tags", resolve: context => context.Source.Tags);
            Field<EnumerationGraphType<Status>>("status");
            Field<StringGraphType>("modified", resolve: context => contextServiceLocator.Humanizer.TimeAgo(context.Source.Modified ?? context.Source.Created));
            Field<IntGraphType>("applicantCount", resolve: context => context.Source.JobApplicants.Count);
        }
    }
}
