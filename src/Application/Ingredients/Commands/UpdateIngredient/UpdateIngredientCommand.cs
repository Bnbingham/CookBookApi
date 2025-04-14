namespace CookBookApi.Application.Ingredients.Commands.UpdateIngredient;

using MediatR;

public class UpdateIngredientCommand : IRequest<bool>
{
    public Guid Id { get; init; }

    public string Name { get; init; }
    public string Description { get; init; }
}
