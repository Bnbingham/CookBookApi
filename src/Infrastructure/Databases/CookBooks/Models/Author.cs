namespace CookBookApi.Infrastructure.Databases.CookBooks.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal record Author : Entity
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public ICollection<Recipe> Recipes { get; init; } = [];
}
