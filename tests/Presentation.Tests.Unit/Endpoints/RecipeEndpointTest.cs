using System;

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
using Entities = Application.Recipes.Entities;
using Queries = Application.Recipes.Queries;

public class RecipeEndpointTest
{
    [Fact]
    public async Task GetRecipes_ShouldReturn_Ok()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetRecipes.GetRecipesQuery>())
            .ReturnsForAnyArgs(
            [
                new Entities.Recipe(Guid.Empty, "Lorem", "Ipsum", "Lorem", Guid.Empty, [])
            ]);

        // Act
        var response = await RecipesEndpoints.GetRecipes(mediator);

        // Assert
        var result = response.ShouldBeOfType<Ok<List<Entities.Recipe>>>();

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        var value = result.Value.ShouldBeOfType<List<Entities.Recipe>>();

        _ = value[0].Id.ShouldBeOfType<Guid>();
        value[0].Id.ShouldBe(Guid.Empty);
        _ = value[0].Title.ShouldBeOfType<string>();
        value[0].Title.ShouldBe("Lorem");
        _ = value[0].Description.ShouldBeOfType<string>();
        value[0].Description.ShouldBe("Ipsum");
        _ = value[0].Instructions.ShouldBeOfType<string>();
        value[0].Instructions.ShouldBe("Lorem");
        _ = value[0].AuthorId.ShouldBeOfType<Guid>();
        value[0].AuthorId.ShouldBe(Guid.Empty);
    }

    [Fact]
    public async Task GetRecipes_ShouldReturn_Problem()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetRecipes.GetRecipesQuery>())
            .ThrowsForAnyArgs(new Exception("An error occurred"));

        // Act
        var response = await RecipesEndpoints.GetRecipes(mediator);

        // Assert
        var result = response.ShouldBeOfType<ProblemHttpResult>();

        result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturn_Ok()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetRecipeById.GetRecipeByIdQuery>())
            .ReturnsForAnyArgs(new Entities.Recipe(Guid.Empty, "Lorem", "Ipsum", "Lorem", Guid.Empty, []));

        // Act
        var response = await RecipesEndpoints.GetRecipeById(Guid.Empty, mediator);

        // Assert
        var result = response.ShouldBeOfType<Ok<Entities.Recipe>>();

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        var value = result.Value.ShouldBeOfType<Entities.Recipe>();

        _ = value.Id.ShouldBeOfType<Guid>();
        value.Id.ShouldBe(Guid.Empty);
        _ = value.Title.ShouldBeOfType<string>();
        value.Title.ShouldBe("Lorem");
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturn_Problem()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetRecipeById.GetRecipeByIdQuery>())
            .ThrowsForAnyArgs(new Exception("An error occurred"));

        // Act  
        var response = await RecipesEndpoints.GetRecipeById(Guid.Empty, mediator);

        // Assert
        var result = response.ShouldBeOfType<ProblemHttpResult>();

        result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
    }

}
