using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer
           (builder.Configuration.GetConnectionString("CommandConStr")));
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>();



var app = builder.Build();

//app.UseRouting().UseEndpoints(endpoints=> endpoints.MapGraphQL());

app.MapGraphQL();

app.UseGraphQLVoyager(new GraphQL.Server.Ui.Voyager.GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });

app.MapGet("/", ([FromServices]AppDbContext context) => {
    return context.Platforms;
});

app.Run();
