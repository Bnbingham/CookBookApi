namespace CookBookApi.Application.Recipes.Queries.GetRecipes;

using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;

public class GetRecipesHandler(IRecipesRepository repository) : IRequestHandler<GetRecipesQuery, List<Recipe>>
{
    public async Task<List<Recipe>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetRecipes(cancellationToken);
    }
}