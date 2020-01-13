using FullStackJobs.GraphQL.Core.Domain.Values;
using System.Collections.Generic;

namespace FullStackJobs.GraphQL.Core.Domain.Entities
{
    public class Job : BaseEntity
    {
        public int Id { get; private set; }
        public string EmployerId { get; private set; }
        public Employer Employer { get; private set; }
        public string Position { get; private set; }
        public string Company { get; private set; }
        public string Icon { get; private set; }
        public string Location { get; private set; }
        public int? AnnualBaseSalary { get; private set; }
        public string Description { get; private set; }
        public string Responsibilities { get; private set; }
        public string Requirements { get; private set; }
        public string ApplicationInstructions { get; private set; }
        public Status Status { get; private set; }

        private readonly List<JobApplicant> _jobApplicants = new List<JobApplicant>();
        public IReadOnlyCollection<JobApplicant> JobApplicants => _jobApplicants.AsReadOnly();

        private readonly List<Tag> _tags = new List<Tag>();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public static Job Build(string employerId, string position, string icon)
        {
            return new Job { EmployerId = employerId, Position = position, Icon = icon };
        }

        public Tag AddTag(string name)
        {
            var tag = new Tag(name);
            _tags.Add(tag);
            return tag;
        }

        public JobApplicant AddJobApplication(int jobId, string applicantId)
        {
            var jobApplicant = new JobApplicant { JobId = jobId, ApplicantId = applicantId };
            _jobApplicants.Add(jobApplicant);
            return jobApplicant;
        }

        public void Update(Job job)
        {
            Company = job.Company;
            Position = job.Position;
            Location = job.Location;
            AnnualBaseSalary = job.AnnualBaseSalary;
            Description = job.Description;
            Responsibilities = job.Responsibilities;
            Requirements = job.Requirements;
            ApplicationInstructions = job.ApplicationInstructions;
            Status = job.Status;
        }
    }
}

