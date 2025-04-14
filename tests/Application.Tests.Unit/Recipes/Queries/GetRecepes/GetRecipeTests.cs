namespace CookBookApi.Application.Tests.Unit.Recipes.Queries.GetRecepes;

using System.Threading;
using System.Threading.Tasks;
using Application.Recipes;
using Application.Recipes.Entities;
using Application.Recipes.Queries.GetRecipes;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetRecipesHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetRecipesQuery();

        var context = Substitute.For<IRecipesRepository>();
        var handler = new GetRecipesHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetRecipes(token).Returns([new Recipe(Guid.Empty, "Title", "Description", "Instructions", Guid.Empty, [])]);

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetRecipes(token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<List<Recipe>>();

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);

        result[0].Id.ShouldBe(Guid.Empty);
        result[0].Title.ShouldBe("Title");
        result[0].Description.ShouldBe("Description");
        result[0].Instructions.ShouldBe("Instructions");
        result[0].RecipeLineItems.ShouldBeEmpty();
    }
}
