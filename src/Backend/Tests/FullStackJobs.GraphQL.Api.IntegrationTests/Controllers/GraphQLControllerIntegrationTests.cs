using System.Linq;
using FullStackJobs.GraphQL.Api.IntegrationTests.Fixtures;
using FullStackJobs.GraphQL.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Testing.Support;
using Xunit;
using System;


namespace FullStackJobs.GraphQL.Api.IntegrationTests.Controllers
{
    public sealed class GraphQLControllerIntegrationTests : IClassFixture<GraphQLApiWebApplicationFactory<AppDbContext>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _dbContext;

        public GraphQLControllerIntegrationTests(GraphQLApiWebApplicationFactory<AppDbContext> factory)
        {
            _client = factory.CreateClient();
            _dbContext = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted(); // https://stackoverflow.com/questions/43406553/xunit-test-dbcontext-did-not-dispose
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CanCreateJob()
        {
            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
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
            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
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

        [Theory]
        [InlineData(1)]
        public async Task CanFetchJob(int id)
        {
            // arrange
            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.SaveChanges();

            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent($@"{{""query"":""query FullStackJobsQuery($id: Int!)
                                                {{
                                                    job(id: $id) {{
                                                        position   
                                                    }}
                                                }}"",
                                                ""variables"":{{""id"":{id}}},
                                                ""operationName"":""FullStackJobsQuery""}}", Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(@"{""data"":{""job"":{""position"":""C# Ninja""}}}", content);

            // Use a separate instance of the context to verify correct data was saved to the database
            await using var context = DbContextFactory.MakeInMemoryProviderDbContext<AppDbContext>(Configuration.InMemoryDatabase);
            var job = context.Jobs.Single(j => j.Id == 1);
            Assert.Equal("123", job.EmployerId);
            Assert.Equal("C# Ninja", job.Position);
        }

        [Fact]
        public async Task CanFetchEmployerJobs()
        {
            // arrange
            _dbContext.Add(EntityFactory.MakeJob("123", "C# Ninja"));
            _dbContext.Add(EntityFactory.MakeEmployer("123"));
            _dbContext.SaveChanges();

            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
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
            _dbContext.Add(EntityFactory.MakeJob("", "C# Ninja", true));
            _dbContext.SaveChanges();

            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/graphql")
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
    }
}


