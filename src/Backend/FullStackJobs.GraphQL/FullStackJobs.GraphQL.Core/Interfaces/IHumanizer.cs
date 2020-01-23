using System;


namespace FullStackJobs.GraphQL.Core.Interfaces
{
    public interface IHumanizer
    {
        string TimeAgo(DateTime dateTime);
    }
}
