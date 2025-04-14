namespace CookBookApi.Application.Ingredients.Commands.CreateIngredient;

using Entities;
using MediatR;

public class CreateIngredientCommand : IRequest<Ingredient>
{
    public string Name { get; init; }

    public string Description { get; init; }
}