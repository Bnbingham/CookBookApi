namespace CookBookApi.Application.Tests.Unit.Recipes.Commands.DeleteRecipe;

using Xunit;
using NSubstitute;
using CookBookApi.Application.Recipes.Commands.DeleteRecipe;
using System.Threading;
using System.Threading.Tasks;
using CookBookApi.Application.Recipes;


public class DeleteRecipeTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Command()
    {
        // Arrange
        var command = new DeleteRecipeCommand { Id = Guid.Empty };
        var recipesRepository = Substitute.For<IRecipesRepository>();

        _ = recipesRepository.RecipeExists(default, default).ReturnsForAnyArgs(true);

        var handler = new DeleteRecipeHandler(recipesRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        _ = await handler.Handle(command, token);

        // Assert
        _ = await recipesRepository.Received(1).RecipeExists(command.Id, token);
        _ = await recipesRepository.Received(1).DeleteRecipe(command.Id, token);
    }

   
}
