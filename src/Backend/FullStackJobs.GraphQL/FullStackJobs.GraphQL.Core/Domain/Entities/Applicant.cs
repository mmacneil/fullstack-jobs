using System.Collections.Generic;

namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public class Applicant : User
    {
        public ICollection<JobApplicant> JobApplicants { get; private set; }

        internal Applicant(string id, string fullName) : base(id, fullName)
        {
        }
    }
}
