namespace CookBookApi.Infrastructure.Tests.Integration.Databases.CookBooks.Extensions;

using System;
using Infrastructure.Databases.CookBooks;
using Infrastructure.Databases.CookBooks.Models;

internal static class CookBooksDbContextExtensions
{
    public static CookBooksDbContext AddTestData(this CookBooksDbContext context)
    {
        var authors = new List<Author>
        {
            new() { Id = Guid.NewGuid(), FirstName = "One", LastName = "One" },
            new() { Id = Guid.NewGuid(), FirstName = "Two", LastName = "Two" },
            new() { Id = Guid.NewGuid(), FirstName = "Three", LastName = "Three" }
        };

        context.Authors.AddRange(authors);

        var cookbooks = new List<CookBook>
        {
            new() { Id = Guid.NewGuid(), Title = "One", Description = "Description One" },
            new() { Id = Guid.NewGuid(), Title = "Two", Description = "Description Two" },
            new() { Id = Guid.NewGuid(), Title = "Three", Description = "Description Three" }
        };

        context.CookBooks.AddRange(cookbooks);

        _ = context.SaveChanges();

        return context;
    }
}
