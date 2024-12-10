using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer
           (builder.Configuration.GetConnectionString("CommandConStr")));
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddFiltering()
                .AddSorting();



var app = builder.Build();

//app.UseRouting().UseEndpoints(endpoints=> endpoints.MapGraphQL());
app.UseAuthentication();

app.Use(async (context, next) =>
{
    if (!context.User.Identity?.IsAuthenticated ?? false)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Not Authenticated");
    }
    else await next();

});

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
