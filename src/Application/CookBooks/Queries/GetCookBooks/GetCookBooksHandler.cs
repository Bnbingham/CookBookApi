namespace CookBookApi.Application.CookBooks.Queries.GetCookBooks;

using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;

public class GetCookBooksHandler(ICookBookRepository repository) : IRequestHandler<GetCookBooksQuery, List<CookBook>>
{
    public async Task<List<CookBook>> Handle(GetCookBooksQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetCookBooks(cancellationToken);
    }
}