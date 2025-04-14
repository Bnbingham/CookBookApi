namespace CookBookApi.Application.CookBooks.Queries.GetCookBookById;

using System.Threading;
using System.Threading.Tasks;
using CookBookApi.Application.Common.Enums;
using CookBookApi.Application.Common.Exceptions;
using Entities;
using MediatR;

public class GetCookBookByIdHandler(ICookBookRepository repository) : IRequestHandler<GetCookBookByIdQuery, CookBook>
{
    public async Task<CookBook> Handle(GetCookBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetCookBookById(request.Id, cancellationToken);

        NotFoundException.ThrowIfNull(result, EntityType.CookBook);

        return result;
    }
}
