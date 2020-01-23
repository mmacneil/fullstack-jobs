using GraphQL.Types;
using System.Security.Claims;
using FullStackJobs.GraphQL.Common.Extensions;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Extensions
{
    public static class GraphQLExtensions
    {
        public static string GetUserId(this ResolveFieldContext<object> context)
        {
            return ((GraphQLUserContext)context.UserContext).User.GetClaimValue(ClaimTypes.NameIdentifier);
        }
    }
}
