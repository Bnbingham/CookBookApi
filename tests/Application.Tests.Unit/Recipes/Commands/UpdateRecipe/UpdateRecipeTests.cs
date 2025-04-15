namespace CookBookApi.Application.Tests.Unit.Recipes.Commands.UpdateRecipe;

using Xunit;
using NSubstitute;
using CookBookApi.Application.Recipes.Commands.UpdateRecipe;
using System.Threading;
using System.Threading.Tasks;
using CookBookApi.Application.Recipes;

public class UpdateRecipeTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Command()
    {
        // Arrange
        var command = new UpdateRecipeCommand { Id = Guid.Empty };
        var recipesRepository = Substitute.For<IRecipesRepository>();

        _ = recipesRepository.RecipeExists(default, default).ReturnsForAnyArgs(true);

        var handler = new UpdateRecipeHandler(recipesRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        _ = await handler.Handle(command, token);

        // Assert
        _ = await recipesRepository.Received(1).RecipeExists(command.Id, token);
        _ = await recipesRepository.Received(1).UpdateRecipe(command.Id, command.Title, command.Description, command.Instructions, command.RecipeLineItems, token);
    }
}
