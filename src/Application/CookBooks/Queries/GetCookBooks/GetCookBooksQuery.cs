namespace CookBookApi.Application.CookBooks.Queries.GetCookBooks;

using Entities;
using MediatR;

public class GetCookBooksQuery : IRequest<List<CookBook>>
{
}