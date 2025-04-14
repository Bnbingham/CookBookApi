namespace CookBookApi.Application.Recipes.Commands.CreateRecipe;

using Entities;
using MediatR;


public class CreateRecipeHandler(IRecipesRepository recipesRepository) : IRequestHandler<CreateRecipeCommand, Recipe>
{
    public async Task<Recipe> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipesRepository.CreateRecipe(request.Title, request.Description, request.Instructions, request.AuthorId, request.RecipeLineItems, request.CookBookIds, cancellationToken);

        return recipe;
    }
}
