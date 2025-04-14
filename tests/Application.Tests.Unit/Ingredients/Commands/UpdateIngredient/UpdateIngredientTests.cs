namespace CookBookApi.Application.Tests.Unit.Ingredients.Commands.UpdateIngredient;

using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients;
using Application.Ingredients.Commands.UpdateIngredient;
using NSubstitute;
using Shouldly;
using Xunit;
using CookBookApi.Application.Common.Exceptions;

public class UpdateIngredientTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Command()
    {
        // Arrange
        var command = new UpdateIngredientCommand
        {
            Id = Guid.Empty,
            Name = "Test",
            Description = "Test"
        };

        var ingredientsRepository = Substitute.For<IIngredientsRepository>();

        _ = ingredientsRepository.IngredientExists(default, default).ReturnsForAnyArgs(true);

        var handler = new UpdateIngredientHandler(ingredientsRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        _ = await handler.Handle(command, token);

        // Assert
        _ = await ingredientsRepository.Received(1).IngredientExists(command.Id, token);
        _ = await ingredientsRepository.Received(1).UpdateIngredient(command.Id, command.Name, command.Description, token);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_IngredientDoesNotExist()
    {
        // Arrange
        var command = new UpdateIngredientCommand
        {
            Id = Guid.Empty,
            Name = "Test",
            Description = "Test"
        };

        var ingredientsRepository = Substitute.For<IIngredientsRepository>();

        _ = ingredientsRepository.IngredientExists(default, default).ReturnsForAnyArgs(false);

        var handler = new UpdateIngredientHandler(ingredientsRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        var exception = Should.Throw<NotFoundException>(async () => await handler.Handle(command, token));

        // Assert
        exception.Message.ShouldBe("The Ingredient with the supplied id was not found.");

        _ = await ingredientsRepository.Received(1).IngredientExists(command.Id, token);
        _ = await ingredientsRepository.DidNotReceive().UpdateIngredient(command.Id, command.Name, command.Description, token);
    }


}
