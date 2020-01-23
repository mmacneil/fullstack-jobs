using FullStackJobs.GraphQL.Core.Interfaces;
using FullStackJobs.GraphQL.Core.Interfaces.Gateways.Repositories;
using Microsoft.AspNetCore.Http;
using GraphQL.Utilities;

namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Helpers
{
    // https://github.com/graphql-dotnet/graphql-dotnet/issues/648#issuecomment-431489339
    public class ContextServiceLocator
    {
        public IJobRepository JobRepository => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IJobRepository>();

        public IHumanizer Humanizer => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IHumanizer>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
