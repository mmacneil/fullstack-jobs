# fullstack-jobs
Real(ish) demo using Angular with ASP.NET Core GraphQL and IdentityServer.

Based on the multi part tutorial series:

Part 1: [Building an ASP.NET Core auth server using IdentityServer](https://fullstackmark.com/post/22/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-1)

Part 2: [Angular app foundation with user signup and login features](https://fullstackmark.com/post/23/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-2)

Part 3: [Implementing an ASP.NET Core GraphQL API with authorization using GraphQL .NET](https://fullstackmark.com/post/24/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-3)

Part 4: [Integrating Angular with a backend GraphQL API using Apollo Client ](https://fullstackmark.com/post/25/build-an-authenticated-graphql-app-with-angular-aspnet-core-and-identityserver-part-4)

## The App
![alt text](https://raw.githubusercontent.com/mmacneil/fullstack-jobs/master/docs/img/angular-aspnet-core-job-application-flow.gif "Build an Authenticated GraphQL App with Angular, ASP.NET Core and IdentityServer")

![alt text](https://github.com/mmacneil/fullstack-jobs/blob/master/docs/img/angular-aspnet-core-job-edit-flow.gif "Build an Authenticated GraphQL App with Angular, ASP.NET Core and IdentityServer")

## Development Environment

- .NET Core 3.1
- Visual Studio Code
- Visual Studio 2019 Professional for Windows
- SQL Server Express 2016 LocalDB
- <a href="https://nodejs.org/en/download/" target="_blank">Node.js with npm</a>
- <a href="https://cli.angular.io/" target="_blank">Angular CLI</a>

## Setup

To build and run the solution:

#### Get the Code

Clone or create a template from this repository.

#### Create the Sql Server Database

Use migrations to create the database as follows:

From the command line use the dotnet CLI to apply the migrations from each project's `Infrastructure` folder.
1. <code>FullStackJobs.AuthServer.Infrastructure> dotnet ef database update --context PersistedGrantDbContext</code>
2. <code>FullStackJobs.AuthServer.Infrastructure> dotnet ef database update --context AppIdentityDbContext</code>
3. <code>FullStackJobs.GraphQL.Infrastructure> dotnet ef database update</code>
   
#### Build and Run the AuthServer and GraphQL API Backend Projects

##### Visual Studio for Windows

Open the `FullStackJobs.sln` solution file which contains both the [AuthServer](https://github.com/mmacneil/fullstack-jobs/tree/master/src/Backend/FullStackJobs.AuthServer) and [GraphQL API](https://github.com/mmacneil/fullstack-jobs/tree/master/src/Backend/FullStackJobs.GraphQL) projects.  You must configure the solution to start up both projects.  Once complete, start the solution in the debugger or use the CLI `dotnet run` command to run them individually.

*todo*: Add instructions for VS Code.

#### Build and Run the Angular Frontend Project

1. Use `npm` to install depdendencies from `package.json'.
<pre><code>Spa> npm install</code></pre>

2. Use the Angular CLI to build and launch the app on the webpack development server.
<pre><code>Spa> ng serve</code></pre>

#### View the App

Point your browser to *https://localhost:4200* to access the application.

#### Host Configuration

The `AuthServer` is configured to run at *https://localhost:8787* while the `GraphQL API` is set to *https://localhost:5025*.

If you need to change these locations for your environment there are several spots in the solution you must update.

*Angular*
 
- The `RESOURCE_BASE_URI` and `AUTH_BASE_URI` values in the [config service](https://github.com/mmacneil/fullstack-jobs/blob/master/src/Frontend/Spa/src/app/core/services/config.service.ts).

*FullStackJobs.GraphQL*

- The OpendIDConnect `Authority` in [Startup](https://github.com/mmacneil/fullstack-jobs/blob/master/src/Backend/FullStackJobs.GraphQL/FullStackJobs.GraphQL.Api/Startup.cs)

<pre><code>services.AddAuthentication(options =&gt;
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =&gt;
{
  o.Authority = &quot;https://localhost:8787&quot;;
  o.Audience = &quot;resourceapi&quot;;
  o.RequireHttpsMetadata = false;
});
</code>
</pre>


#### Contact

mark@fullstackmark.com
