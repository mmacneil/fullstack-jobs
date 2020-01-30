using FullStackJobs.GraphQL.Core.Domain.Entities;
using Newtonsoft.Json;
using Testing.Support;

namespace FullStackJobs.GraphQL.Api.IntegrationTests
{
    public static class EntityFactory
    {
        public static Job MakeJob(string employerId, bool published = false)
        {
            if (published)
            {
                return JsonConvert.DeserializeObject<Job>(@"{""Id"": 1, ""Position"": ""GraphQL Pro"", ""Status"": ""Published"" }", new JsonSerializerSettings()
                {
                    ContractResolver = new PrivateResolver()
                });
            }

            return Job.Build(employerId, "C# Ninja", "logo.png");
        }

        public static Employer MakeEmployer(string id)
        {
            return new Employer(id, "Test Employer");
        }
    }
}
