namespace CookBookApi.Application.CookBooks.Queries.GetCookBookById;

using System.ComponentModel.DataAnnotations;
using Entities;
using MediatR;

public class GetCookBookByIdQuery : IRequest<CookBook>
{
    [Required]
    public Guid Id { get; init; }
}