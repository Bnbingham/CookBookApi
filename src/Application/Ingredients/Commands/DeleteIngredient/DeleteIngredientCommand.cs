namespace CookBookApi.Application.Ingredients.Commands.DeleteIngredient;

using MediatR;

public class DeleteIngredientCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}
