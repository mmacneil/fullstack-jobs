using FullStackJobs.GraphQL.Common;
using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Extensions;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Types;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Types.Input;
using GraphQL.Authorization;
using GraphQL.Types;
using System;
using System.Collections.Generic;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL
{
    public class FullStackJobsMutation : ObjectGraphType
    {
        public FullStackJobsMutation(ContextServiceLocator contextServiceLocator)
        {
            FieldAsync<JobSummaryType>(
                "createApplication",
                 arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CreateApplicationInputType>> { Name = "input" }),
                resolve: async context =>
                {
                    var input = context.GetArgument<dynamic>("input");
                    var job = await contextServiceLocator.JobRepository.GetById(int.Parse(input["jobId"]));
                    job.AddJobApplication(job.Id, context.GetUserId());
                    await contextServiceLocator.JobRepository.Update(job);
                    return job;
                }).AuthorizeWith(Policies.Applicant);

            FieldAsync<JobType>(
              "createJob",
              resolve: async context => await contextServiceLocator.JobRepository.Add(Job.Build(context.GetUserId(),
                  "Untitled Position", $"icon-{new Random().Next(1, 4)}.png"))).AuthorizeWith(Policies.Employer);

            FieldAsync<JobType>(
                "updateJob",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UpdateJobInputType>> { Name = "input" }),
                resolve: async context =>
                {
                    var input = context.GetArgument<Job>("input");
                    var job = await contextServiceLocator.JobRepository.GetById(input.Id);
                    job.Update(input);

                    // For demo purposes we're just adding newly entered tags (see below: where no id).
                    // Manually/awkwardly resolve and add tags ReadOnlyCollection on Job entity can't be coerced within GetArgument
                    if ((context.Arguments["input"] as Dictionary<string, object>)?["tags"] is List<object> tags)
                    {
                        foreach (Dictionary<string, object> tag in tags)
                        {
                            if (!tag.ContainsKey("id"))
                            {
                                job.AddTag(tag["name"].ToString());
                            }
                        }
                    }

                    await contextServiceLocator.JobRepository.Update(job);
                    return job;

                }).AuthorizeWith(Policies.Employer);
        }
    }
}
