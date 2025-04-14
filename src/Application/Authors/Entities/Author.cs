namespace CookBookApi.Application.Authors.Entities;

using Application.Recipes.Entities;
public record Author(Guid Id, string FirstName, string LastName, ICollection<AuthorRecipe> Recipes);
