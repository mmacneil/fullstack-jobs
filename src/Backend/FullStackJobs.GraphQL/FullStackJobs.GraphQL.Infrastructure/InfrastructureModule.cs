using Autofac;
using FullStackJobs.GraphQL.Core.Interfaces;
using FullStackJobs.GraphQL.Core.Interfaces.Gateways.Repositories;
using FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers;

namespace FullStackJobs.GraphQL.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(new[] { System.Reflection.Assembly.GetExecutingAssembly() }).AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces();
            builder.RegisterType<Utilities.Humanizer>().As<IHumanizer>().SingleInstance();
            builder.RegisterType<ContextServiceLocator>().SingleInstance();
        }
    }
}
