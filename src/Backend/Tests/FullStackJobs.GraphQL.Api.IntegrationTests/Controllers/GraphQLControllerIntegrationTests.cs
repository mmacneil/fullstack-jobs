using System.Linq;
using FullStackJobs.GraphQL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Testing.Support;
using Xunit;
using System;
using System.Net;

namespace FullStackJobs.GraphQL.Api.IntegrationTests.Controllers
{
    public sealed class GraphQLControllerIntegrationTests : IntegrationTestBase<AppDbContext, Startup>, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public GraphQLControllerIntegrationTests(FullStackJobsApplicationFactory<Startup> factory) : base(factory)
        {
            _dbContext = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted(); // https://stackoverflow.com/questions/43406553/xunit-test-dbcontext-did-not-dispose
            _dbContext.Dispose();
        }

        #region Mutations

        [Fact]
        public async Task CanCreateJob()
        {
            // arrange 
            var client = GetFactory(isEmployer: true).CreateClient();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@" { ""query"": 
                                                ""mutation($input: CreateJobInput!) {
                                                   createJob(input: $input) {
                                                     id
                                                     position
                                                   }
                                                }"",""variables"":null}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""createJob"":{""id"":1,""position"":""Untitled Position""}}}", content);

            // Verify correct data was saved to the database
            var job = _dbContext.Jobs.First();
            Assert.Equal("123", job.EmployerId);
            Assert.Equal("Untitled Position", job.Position);
        }

        [Fact]
        public async Task CanUpdateJob()
        {
            // arrange 
            var client = GetFactory(isEmployer: true).CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@" { ""query"": 
                                                ""mutation($input: UpdateJobInput!) {
                                                   updateJob(input: $input) 
                                                }"",""variables"":{""input"": {""id"": 1, ""position"": ""test-update"", ""status"": ""PUBLISHED"", ""tags"" : [] }} }", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.Single(j => j.Id == 1);
            Assert.Equal("123", job.EmployerId);
            Assert.Equal("test-update", job.Position);
        }

        [Fact]
        public async Task CanCreateApplication()
        {
            // arrange 
            var client = GetFactory(isApplicant: true).CreateClient();
            _dbContext.Add(EntityFactory.MakeJob("1", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@" { ""query"": 
                                                ""mutation($input: CreateApplicationInput!) {
                                                   createApplication(input: $input) { 
                                                     id,
                                                     position,   
                                                     applicantCount
                                                   }
                                                }"",""variables"":{""input"": {""jobId"": 1}} }", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.Include(j => j.JobApplicants).Single(j => j.Id == 1);
            Assert.Single(job.JobApplicants);
            Assert.Equal("123", job.JobApplicants.First().ApplicantId); // The id value obtained from the user claim
        }

        #endregion Mutations

        #region Queries

        [Theory]
        [InlineData(1)]
        public async Task CanFetchJob(int id)
        {
            // arrange 
            var client = GetFactory().CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent($@"{{""query"":""query FullStackJobsQuery($id: Int!)
                                                {{
                                                    job(id: $id) {{
                                                        id
                                                        position   
                                                    }}
                                                }}"",
                                                ""variables"":{{""id"":{id}}},
                                                ""operationName"":""FullStackJobsQuery""}}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""job"":{""id"":1,""position"":""C# Ninja""}}}", content);          
        }

        [Theory]
        [InlineData(1)]
        public async Task CantFetchAnnualBaseSalaryAsApplicant(int id)
        {
            // arrange 
            // acting as an applicant
            var client = GetFactory(isApplicant: true).CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent($@"{{""query"":""query FullStackJobsQuery($id: Int!)
                                                {{
                                                    job(id: $id) {{
                                                        id
                                                        position
                                                        annualBaseSalary
                                                    }}
                                                }}"",
                                                ""variables"":{{""id"":{id}}},
                                                ""operationName"":""FullStackJobsQuery""}}", Encoding.UTF8, "application/json")
            });

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);            
        }

        [Theory]
        [InlineData(1)]
        public async Task CanFetchAnnualBaseSalaryAsEmployer(int id)
        {
            // arrange 
            // acting as an employer
            var client = GetFactory(isEmployer: true).CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent($@"{{""query"":""query FullStackJobsQuery($id: Int!)
                                                {{
                                                    job(id: $id) {{
                                                        id
                                                        position
                                                        annualBaseSalary
                                                    }}
                                                }}"",
                                                ""variables"":{{""id"":{id}}},
                                                ""operationName"":""FullStackJobsQuery""}}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""job"":{""id"":1,""position"":""C# Ninja"",""annualBaseSalary"":null}}}", content);
        }

        [Fact]
        public async Task CanFetchEmployerJobs()
        {
            // arrange 
            var client = GetFactory(isEmployer: true).CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.Add(EntityFactory.MakeEmployer("123"));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@"{""query"":""query FullStackJobsQuery
                                                {
                                                    employerJobs {
                                                        id
                                                        position
                                                        status
                                                    }
                                                }"",
                                                ""variables"":null,
                                                ""operationName"":""FullStackJobsQuery""}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""employerJobs"":[{""id"":1,""position"":""C# Ninja"",""status"":""DRAFT""}]}}", content);
        }

        [Fact]
        public async Task CanFetchPublicJobs()
        {
            // arrange 
            var client = GetFactory().CreateClient();

            _dbContext.Add(EntityFactory.MakeJob("", "C# Ninja", true));
            _dbContext.SaveChanges();

            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(@"{""query"":""query FullStackJobsQuery
                                                {
                                                    publicJobs {
                                                        id
                                                        position
                                                        status
                                                    }
                                                }"",
                                                ""variables"":null,
                                                ""operationName"":""FullStackJobsQuery""}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""publicJobs"":[{""id"":1,""position"":""C# Ninja"",""status"":""PUBLISHED""}]}}", content);
        }

        #endregion Queries
    }
}


