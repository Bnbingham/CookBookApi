namespace CookBookApi.Application.CookBooks.Entities;

using Application.Recipes.Entities;

public record CookBook(Guid Id, string Title, string Description, ICollection<CookBookRecipe> Recipes = null);