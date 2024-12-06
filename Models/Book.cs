using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.Models
{
    public record Book(string Title, Author Author);
    public record Author(string Name, DateTime Birthday);
}