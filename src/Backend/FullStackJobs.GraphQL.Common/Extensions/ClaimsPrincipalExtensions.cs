using System.Linq;
using System.Security.Claims;


namespace FullStackJobs.GraphQL.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == claimType).Single().Value;
        }
    }
}
