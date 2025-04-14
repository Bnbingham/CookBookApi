namespace CookBookApi.Infrastructure.Databases.CookBooks.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal record Ingredient : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<RecipeLineItem> RecipeLineItems { get; init; } = [];
}
