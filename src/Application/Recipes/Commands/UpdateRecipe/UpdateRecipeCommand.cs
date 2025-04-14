namespace CookBookApi.Application.Recipes.Commands.UpdateRecipe;

using CookBookApi.Application.RecipeLineItems.Entities;
using MediatR;


public class UpdateRecipeCommand : IRequest<bool>
{
    public Guid Id { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public string Instructions { get; init; }

    public List<RecipeLineItem> RecipeLineItems { get; init; }

    public List<Guid> CookBookIds { get; init; }
}
