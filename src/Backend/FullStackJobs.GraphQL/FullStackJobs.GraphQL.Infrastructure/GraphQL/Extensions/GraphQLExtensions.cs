using GraphQL.Types;
using System.Security.Claims;
using FullStackJobs.GraphQL.Common.Extensions;
using System;

namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Extensions
{
    public static class GraphQLExtensions
    {
        public static string GetUserId(this ResolveFieldContext<object> context)
        {
            try
            {
                var ss = ((GraphQLUserContext)context.UserContext).User.GetClaimValue(ClaimTypes.NameIdentifier); 
            }
            catch(Exception e)
            {
                string msg = e.Message;
            }
            return ((GraphQLUserContext)context.UserContext).User.GetClaimValue(ClaimTypes.NameIdentifier);
        }
    }
}
