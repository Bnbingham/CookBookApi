namespace CookBookApi.Application.Recipes.Commands.DeleteRecipe;

using MediatR;
using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Application.Common.Enums;

public class DeleteRecipeHandler(IRecipesRepository recipesRepository) : IRequestHandler<DeleteRecipeCommand, bool>
{
    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        if (!await recipesRepository.RecipeExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Recipe);
        }

        return await recipesRepository.DeleteRecipe(request.Id, cancellationToken);
    }
}
