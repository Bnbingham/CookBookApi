namespace CookBookApi.Application.Tests.Unit.Recipes.Queries.GetRecepeById;

using System.Threading;
using System.Threading.Tasks;
using Application.Recipes;
using Application.Recipes.Entities;
using Application.Recipes.Queries.GetRecipeById;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetRecipeByIdTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetRecipeByIdQuery { Id = Guid.Empty };

        var context = Substitute.For<IRecipesRepository>();
        var handler = new GetRecipeByIdHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetRecipeById(Guid.Empty, token).Returns(new Recipe(Guid.Empty, "Title", "Description", "Instructions", Guid.Empty, []));

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetRecipeById(Guid.Empty, token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<Recipe>();

        result.Id.ShouldBe(Guid.Empty);
        result.Title.ShouldBe("Title");
        result.Description.ShouldBe("Description");
        result.Instructions.ShouldBe("Instructions");
        result.RecipeLineItems.ShouldBeEmpty();
    }
}
