using GraphQL.Authorization;
using System.Collections.Generic;
using System.Security.Claims;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL
{
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
