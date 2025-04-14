namespace CookBookApi.Application.Recipes.Queries.GetRecipeById;

using System.ComponentModel.DataAnnotations;
using Entities;
using MediatR;

public class GetRecipeByIdQuery : IRequest<Recipe>
{
    [Required]
    public Guid Id { get; init; }
}
