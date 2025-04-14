namespace CookBookApi.Application.Recipes.Queries.GetRecipeById;

using System.Threading;
using System.Threading.Tasks;
using CookBookApi.Application.Common.Enums;
using CookBookApi.Application.Common.Exceptions;
using CookBookApi.Application.Recipes.Entities;
using MediatR;

public class GetRecipeByIdHandler(IRecipesRepository repository) : IRequestHandler<GetRecipeByIdQuery, Recipe>
{
    public async Task<Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetRecipeById(request.Id, cancellationToken);

        NotFoundException.ThrowIfNull(result, EntityType.Recipe);

        return result;
    }
}
