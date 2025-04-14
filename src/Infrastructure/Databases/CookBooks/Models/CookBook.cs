namespace CookBookApi.Infrastructure.Databases.CookBooks.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal record CookBook : Entity
{
    public string Title { get; init; }

    public string Description { get; init; }

    public ICollection<Recipe> Recipes { get; init; } = [];
}
