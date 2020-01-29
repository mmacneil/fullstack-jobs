using FullStackJobs.GraphQL.Common;
using GraphQL.Authorization;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddGraphQLAuth(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();

                authSettings.AddPolicy(Policies.Employer, _ => _.RequireClaim(ClaimTypes.Role, "employer"));
                authSettings.AddPolicy(Policies.Applicant, _ => _.RequireClaim(ClaimTypes.Role, "applicant"));

                return authSettings;
            });
        }
    }
}
