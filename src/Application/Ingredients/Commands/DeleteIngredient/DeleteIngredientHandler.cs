namespace CookBookApi.Application.Ingredients.Commands.DeleteIngredient;

using MediatR;
using CookBookApi.Application.Ingredients;
using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Application.Common.Enums;

public class DeleteIngredientHandler(IIngredientsRepository ingredientsRepository) : IRequestHandler<DeleteIngredientCommand, bool>
{
    public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        if (!await ingredientsRepository.IngredientExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Ingredient);
        }

        return await ingredientsRepository.DeleteIngredient(request.Id, cancellationToken);
    }
}
