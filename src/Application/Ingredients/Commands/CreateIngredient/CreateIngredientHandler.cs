namespace CookBookApi.Application.Ingredients.Commands.CreateIngredient;

using Entities;
using MediatR;

public class CreateIngredientHandler(IIngredientsRepository ingredientsRepository) : IRequestHandler<CreateIngredientCommand, Ingredient>
{
    public async Task<Ingredient> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientsRepository.CreateIngredient(request.Name, request.Description, cancellationToken);

        return ingredient;
    }
}
