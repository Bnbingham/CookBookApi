namespace CookBookApi.Presentation.Tests.Unit.Endpoints;

using System.Threading.Tasks;
using CookBookApi.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Presentation.Endpoints;
using Shouldly;
using Xunit;
using Entities = Application.Ingredients.Entities;
using Queries = Application.Ingredients.Queries;

public class IngredientEndpointTests
{
    [Fact]
    public async Task GetIngredients_ShouldReturn_Ok()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetIngredients.GetIngredientsQuery>())
            .ReturnsForAnyArgs(
            [
                new Entities.Ingredient(Guid.Empty, "Lorem", "Ipsum")
            ]);

        // Act
        var response = await IngredientsEndpoints.GetIngredients(mediator);

        // Assert
        var result = response.ShouldBeOfType<Ok<List<Entities.Ingredient>>>();

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        var value = result.Value.ShouldBeOfType<List<Entities.Ingredient>>();

        _ = value[0].Id.ShouldBeOfType<Guid>();
        value[0].Id.ShouldBe(Guid.Empty);
        _ = value[0].Name.ShouldBeOfType<string>();
        value[0].Name.ShouldBe("Lorem");
        _ = value[0].Description.ShouldBeOfType<string>();
        value[0].Description.ShouldBe("Ipsum");
    }


    [Fact]
    public async Task GetIngredients_ShouldReturn_Problem()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetIngredients.GetIngredientsQuery>())
            .ThrowsForAnyArgs(new Exception("An error occurred"));

        // Act
        var response = await IngredientsEndpoints.GetIngredients(mediator);

        // Assert
        var result = response.ShouldBeOfType<ProblemHttpResult>();

        result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
    }







}
