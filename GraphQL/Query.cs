using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [HotChocolate.Data.UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([HotChocolate.ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }

        readonly List<Book> _books =
        [
            new("C# In Depth", new Author("Jon Skeet", DateTime.Now)),
            new("Head First C#", new Author("Andrew Stellman", DateTime.Now)),
            new("Full Stack Serverless", new Author("Nader Dabit", DateTime.Now)),
        ];

        [GraphQLDescription("Returns all Books")]
        [UseSorting]
        [UseFiltering]
        public IEnumerable<Book> GetBooks() => _books;

        [GraphQLDescription("Returns the specified Book")]
        public Book GetBook([GraphQLDescription("Title of the Book")] string title) =>
            _books.First(x => x.Title == title);

        [GraphQLDescription("Returns the specified Author")]
        public Author GetAuthor([GraphQLDescription("Name of the Author")] string name) =>
            _books.First(x => x.Author.Name == name).Author;
    }
}