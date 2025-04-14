namespace CookBookApi.Application.Recipes.Commands.CreateRecipe;

using CookBookApi.Application.RecipeLineItems.Entities;
using Entities;
using MediatR;

public class CreateRecipeCommand : IRequest<Recipe>
{
    public string Title { get; init; }

    public string Description { get; init; }

    public string Instructions { get; init; }

    public Guid AuthorId { get; init; }

    public List<Guid> CookBookIds { get; init; }

    public List<RecipeLineItem> RecipeLineItems { get; init; }
}
