using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Testing.Support.UserFilters
{
    public class ApplicantUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "123"),
                new Claim(ClaimTypes.Name, "Jon Doe"),
                new Claim(ClaimTypes.Email, "applicant@fullstackjobs.com"),
                new Claim(ClaimTypes.Role, "applicant")
            }));

            await next();
        }
    }
}
