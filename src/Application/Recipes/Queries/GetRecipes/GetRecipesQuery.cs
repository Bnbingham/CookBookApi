namespace CookBookApi.Application.Recipes.Queries.GetRecipes;

using Entities;
using MediatR;

public class GetRecipesQuery : IRequest<List<Recipe>>
{
}