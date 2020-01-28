using System.Linq;
using System.Security.Claims;


namespace FullStackJobs.GraphQL.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.Claims.Single(c => c.Type == claimType).Value;
        }
    }
}
