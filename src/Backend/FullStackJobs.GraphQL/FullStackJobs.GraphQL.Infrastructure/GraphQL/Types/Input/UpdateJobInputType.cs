using FullStackJobs.GraphQL.Core.Domain.Entities;
using FullStackJobs.GraphQL.Core.Domain.Values;
using GraphQL.Types;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types.Input
{
    public class UpdateJobInputType : InputObjectGraphType<Job>
    {
        public UpdateJobInputType()
        {
            Name = "UpdateJobInput";
            Field(x => x.Id);
            Field(x => x.Company, true);
            Field(x => x.Position, true);
            Field(x => x.Location, true);
            Field(x => x.AnnualBaseSalary, true);
            Field(x => x.Description, true);
            Field(x => x.Responsibilities, true);
            Field(x => x.Requirements, true);
            Field(x => x.ApplicationInstructions, true);
            Field<EnumerationGraphType<Status>>("status");
            Field<ListGraphType<TagInputType>>("tags");
        }
    }
}
