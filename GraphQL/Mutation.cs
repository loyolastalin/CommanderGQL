using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Adds a platform.")]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext context,
            CancellationToken cancellationToken
            ) 
            {
                var platform = new Platform{
                    Name = input.Name,
                    LicenseKey = input.LicenseKey
                };

                context.Platforms.Add(platform);
                await context.SaveChangesAsync(cancellationToken);

                return new AddPlatformPayload(platform);
            }
    }
}