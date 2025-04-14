namespace CookBookApi.Application.Ingredients.Queries.GetIngredients;

using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;

public class GetIngredientsHandler(IIngredientsRepository repository) : IRequestHandler<GetIngredientsQuery, List<Ingredient>>
{
    public async Task<List<Ingredient>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetIngredients(cancellationToken);
    }
}