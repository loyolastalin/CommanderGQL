using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer
           (builder.Configuration.GetConnectionString("CommandConStr")));
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting();



var app = builder.Build();

//app.UseRouting().UseEndpoints(endpoints=> endpoints.MapGraphQL());

app.MapGraphQL();

app.UseGraphQLVoyager(new GraphQL.Server.Ui.Voyager.GraphQLVoyagerOptions()
{
    GraphQLEndPoint = "/graphql",
    Path = "/graphql-voyager"
});

app.MapGet("/", ([FromServices] IDbContextFactory<AppDbContext> factory) =>
{
    using var context = factory.CreateDbContext();
    return context.Platforms.ToList();

}); 

app.Run();
