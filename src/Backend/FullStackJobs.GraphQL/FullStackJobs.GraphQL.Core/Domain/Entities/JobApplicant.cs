
namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public class JobApplicant
    {
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
