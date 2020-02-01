using FullStackJobs.GraphQL.Core.Domain.Values;
using FullStackJobs.GraphQL.Core.Specifications;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Extensions;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Types;
using GraphQL.Types;
using System.Linq;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL
{
    public class FullStackJobsQuery : ObjectGraphType
    {
        public FullStackJobsQuery(ContextServiceLocator contextServiceLocator)
        {
            FieldAsync<JobType>("job",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),               
                resolve: async context => await contextServiceLocator.JobRepository.GetSingleBySpec(new JobSpecification(j => j.Id == context.GetArgument<int>("id", default))));

            FieldAsync<ListGraphType<JobSummaryType>>("employerJobs",
             resolve: async context =>
             {
                 // Extract the user id from the name claim to fetch the target employer's jobs
                 var jobs = await contextServiceLocator.JobRepository.List(new JobSpecification(j => j.Employer.Id == context.GetUserId()));
                 return jobs.OrderByDescending(j => j.Modified);
             });

            FieldAsync<ListGraphType<JobSummaryType>>("publicJobs",
                resolve: async context =>
                {
                    // Fetch published Jobs from all employers
                    var jobs = await contextServiceLocator.JobRepository.List(new JobSpecification(j => j.Status == Status.Published));
                    return jobs.OrderByDescending(j => j.Modified);
                });
        }
    }
}
