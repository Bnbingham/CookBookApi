namespace CookBookApi.Application.Ingredients.Commands.UpdateIngredient;

using CookBookApi.Application.Common.Enums;
using CookBookApi.Application.Common.Exceptions;
using MediatR;

public class UpdateIngredientHandler(IIngredientsRepository ingredientsRepository) : IRequestHandler<UpdateIngredientCommand, bool>
{
    public async Task<bool> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        if (!await ingredientsRepository.IngredientExists(request.Id, cancellationToken))
        {
            NotFoundException.Throw(EntityType.Ingredient);
        }
        return await ingredientsRepository.UpdateIngredient(request.Id, request.Name, request.Description, cancellationToken);
    }
}
