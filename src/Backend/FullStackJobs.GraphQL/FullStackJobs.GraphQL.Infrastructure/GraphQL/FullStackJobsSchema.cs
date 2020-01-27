using GraphQL;
using GraphQL.Types;
using System;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL
{
    public class FullStackJobsSchema : Schema
    {
        public FullStackJobsSchema(IServiceProvider services) : base(services)
        {
            Query = services.GetService(typeof(FullStackJobsQuery)).As<IObjectGraphType>();
            Mutation = services.GetService(typeof(FullStackJobsMutation)).As<IObjectGraphType>();
        }
    }
}
