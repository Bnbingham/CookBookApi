namespace CookBookApi.Application.Tests.Unit.Ingredients.Queries.GetIngredients;

using System.Threading;
using System.Threading.Tasks;
using Application.Ingredients;
using Application.Ingredients.Entities;
using Application.Ingredients.Queries.GetIngredients;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetIngredientsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetIngredientsQuery();

        var context = Substitute.For<IIngredientsRepository>();
        var handler = new GetIngredientsHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetIngredients(token).Returns([new Ingredient(Guid.Empty, "Name", "Description")]);

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetIngredients(token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<List<Ingredient>>();

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);

        result[0].Id.ShouldBe(Guid.Empty);
        result[0].Name.ShouldBe("Name");
        result[0].Description.ShouldBe("Description");
    }
}
