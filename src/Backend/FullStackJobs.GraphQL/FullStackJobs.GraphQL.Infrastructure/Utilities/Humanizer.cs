using FullStackJobs.GraphQL.Core.Interfaces;
using Humanizer;
using System;


namespace FullStackJobs.GraphQL.Infrastructure.Utilities
{
    public sealed class Humanizer : IHumanizer
    {
        public string TimeAgo(DateTime dateTime)
        {
            return dateTime.Humanize();
        }
    }
}
