using Autofac;
using FullStackJobs.GraphQL.Core.Domain.Values;
using FullStackJobs.GraphQL.Core.Interfaces;
using FullStackJobs.GraphQL.Core.Interfaces.Gateways.Repositories;
using FullStackJobs.GraphQL.Infrastructure.GraphQL;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Types;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Types;

namespace FullStackJobs.GraphQL.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces();
            builder.RegisterType<Utilities.Humanizer>().As<IHumanizer>().SingleInstance();
            builder.RegisterType<ContextServiceLocator>().SingleInstance();
            builder.RegisterType<FullStackJobsSchema>().As<ISchema>().SingleInstance();
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>().SingleInstance();
            builder.RegisterType<FullStackJobsQuery>().SingleInstance();
            builder.RegisterType<FullStackJobsMutation>().SingleInstance();
            builder.RegisterType<JobType>().SingleInstance();
            builder.RegisterType<JobSummaryType>().SingleInstance();
            builder.RegisterType<EnumerationGraphType<Status>>().SingleInstance();
            builder.RegisterType<TagType>().SingleInstance();
            builder.RegisterType<UpdateJobInputType>().SingleInstance();
            builder.RegisterType<TagInputType>().SingleInstance();
            builder.RegisterType<CreateApplicationInputType>().SingleInstance();
        }
    }
}
