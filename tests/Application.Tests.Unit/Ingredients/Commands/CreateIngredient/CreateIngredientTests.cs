namespace CookBookApi.Application.Tests.Unit.Ingredients.Commands.CreateIngredient;

using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients;
using Application.Ingredients.Commands.CreateIngredient;
using NSubstitute;
using Xunit;

public class CreateIngredientTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Command()
    {
        // Arrange
        var command = new CreateIngredientCommand
        {
            Name = "Test",
            Description = "Test"
        };

        var ingredientsRepository = Substitute.For<IIngredientsRepository>();

        _ = ingredientsRepository.IngredientExists(default, default).ReturnsForAnyArgs(true);

        var handler = new CreateIngredientHandler(ingredientsRepository);
        var token = new CancellationTokenSource().Token;

        // Act
        _ = await handler.Handle(command, token);


        // Assert
        _ = await ingredientsRepository.Received(1).CreateIngredient(command.Name, command.Description, token);
    }
}
