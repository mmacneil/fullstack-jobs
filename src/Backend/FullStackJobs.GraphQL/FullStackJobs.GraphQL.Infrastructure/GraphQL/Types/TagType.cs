using FullStackJobs.GraphQL.Core.Domain.Values;
using GraphQL.Types;

namespace FullStackJobs.GraphQL.Infrastructure.GraphQL.Types
{
    public class TagType : ObjectGraphType<Tag>
    {
        public TagType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}
