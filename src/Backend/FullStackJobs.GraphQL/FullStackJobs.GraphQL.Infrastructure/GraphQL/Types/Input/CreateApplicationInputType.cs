using GraphQL.Types;


namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types.Input
{
    public class CreateApplicationInputType : InputObjectGraphType
    {
        public CreateApplicationInputType()
        {
            Name = "CreateApplicationInput";
            Field<NonNullGraphType<IdGraphType>>("jobId");
        }
    }
}
