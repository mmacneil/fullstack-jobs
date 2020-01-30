using FullStackJobs.GraphQL.Core.Domain.Values;
using GraphQL.Types;

namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types.Input
{
    public class TagInputType : InputObjectGraphType<Tag>
    {
        public TagInputType()
        {
            Field(x => x.Id, true);
            Field(x => x.Name);
        }
    }
}
