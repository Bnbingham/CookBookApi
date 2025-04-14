namespace CookBookApi.Application.Ingredients.Queries.GetIngredients;

using Entities;
using MediatR;

public class GetIngredientsQuery : IRequest<List<Ingredient>>
{
}