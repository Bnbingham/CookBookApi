namespace CookBookApi.Application.Recipes.Commands.DeleteRecipe;

using MediatR;

public class DeleteRecipeCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}
