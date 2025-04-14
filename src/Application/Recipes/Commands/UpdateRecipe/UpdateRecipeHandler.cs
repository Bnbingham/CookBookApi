namespace CookBookApi.Application.Recipes.Commands.UpdateRecipe;

using MediatR;
using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Application.Common.Enums;

public class UpdateRecipeHandler(IRecipesRepository recipesRepository) : IRequestHandler<UpdateRecipeCommand, bool>
{
    public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        if (!await recipesRepository.RecipeExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Recipe);
        }

        var recipe = await recipesRepository.UpdateRecipe(request.Id, request.Title, request.Description, request.Instructions, request.RecipeLineItems, cancellationToken);

        return recipe;
    }
}
