namespace CookBookApi.Application.Recipes.Entities;

using Application.RecipeLineItems.Entities;

public record Recipe(Guid Id, string Title, string Description, string Instructions, Guid AuthorId, ICollection<RecipeLineItem> RecipeLineItems);
