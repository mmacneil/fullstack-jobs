using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace FullStackJobs.AuthServer.IntegrationTests.Shared
{
    public static class Helpers
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string GetProjectPath(Assembly startupAssembly)
        {
            //Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            //Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            //Find the folder which contains the solution file. 
            //We then use this information to find the target project which we want to test
            DirectoryInfo directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                //find *.sln files
                var solutionFileInfo = directoryInfo.GetFiles("*.sln").FirstOrDefault();
                if (solutionFileInfo != null)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, projectName));
                }
                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}");
        }
    }
}
