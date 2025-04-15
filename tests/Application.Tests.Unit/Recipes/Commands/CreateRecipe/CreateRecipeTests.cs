namespace CookBookApi.Application.Tests.Unit.Recipes.Commands.CreateRecipe;

using Xunit;
using NSubstitute;
using CookBookApi.Application.Recipes.Commands.CreateRecipe;
using System.Threading;
using System.Threading.Tasks;
using CookBookApi.Application.Recipes;

public class CreateRecipeTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Command()
    {
        // Arrange
        var command = new CreateRecipeCommand
        {
            Title = "Test",
            Description = "Test",
            AuthorId = Guid.Empty,
            RecipeLineItems = [],
            Instructions = "Test"
        };

        var recipesRepository = Substitute.For<IRecipesRepository>();

        var handler = new CreateRecipeHandler(recipesRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        _ = await handler.Handle(command, token);

        // Assert
        _ = await recipesRepository.Received(1).CreateRecipe(command.Title, command.Description, command.Instructions, command.AuthorId, command.RecipeLineItems, command.CookBookIds, token);
    }
}
