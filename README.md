# fullstack-jobs
Real(ish) demo using Angular with ASP.NET Core GraphQL and IdentityServer.

Based on the multi part tutorial series:

Part 1: [Building an ASP.NET Core auth server using IdentityServer](https://fullstackmark.com/post/22/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-1)

Part 2: [Angular app foundation with user signup and login features ](https://fullstackmark.com/post/23/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-2)

## Development Environment

- .NET Core 3.1
- Visual Studio Code
- Visual Studio 2019 Professional for Windows
- SQL Server Express 2016 LocalDB
- <a href="https://nodejs.org/en/download/" target="_blank">Node.js with npm</a>
- <a href="https://cli.angular.io/" target="_blank">Angular CLI</a>

## Setup

1. Clone or create a template from this repository.
2. Use migrations to create the database.

   From the command line use the dotnet tooling to apply the migrations from each project's `Infrastructure` folder.
   1. <code>FullStackJobs.AuthServer.Infrastructure> dotnet ef database update --context PersistedGrantDbContext</code>
   2. <code>FullStackJobs.AuthServer.Infrastructure> dotnet ef database update --context AppIdentityDbContext</code>

![alt text](https://raw.githubusercontent.com/mmacneil/fullstack-jobs/master/docs/img/angular-aspnet-core-job-application-flow.gif "Build an Authenticated GraphQL App with Angular, ASP.NET Core and IdentityServer")

![alt text](https://github.com/mmacneil/fullstack-jobs/blob/master/docs/img/angular-aspnet-core-job-edit-flow.gif "Build an Authenticated GraphQL App with Angular, ASP.NET Core and IdentityServer")
