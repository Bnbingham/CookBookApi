namespace CookBookApi.Presentation.Tests.Unit.Endpoints;

using CookBookApi.Presentation.Endpoints;
using MediatR;
using NSubstitute;
using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Entities = Application.CookBooks.Entities;
using Queries = Application.CookBooks.Queries;


public class CookBookEndpointTests
{
    [Fact]
    public async Task GetCookBook_ShouldReturn_Ok()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        _ = mediator
            .Send(Arg.Any<Queries.GetCookBooks.GetCookBooksQuery>())
            .ReturnsForAnyArgs(
            [
                new Entities.CookBook(Guid.Empty, "Lorem", "Ipsum", [])
            ]);

        // Act
        var response = await CookBooksEndpoints.GetCookBooks(mediator);

        // Assert
        var result = response.ShouldBeOfType<Ok<List<Entities.CookBook>>>();

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        var value = result.Value.ShouldBeOfType<List<Entities.CookBook>>();

        _ = value[0].Id.ShouldBeOfType<Guid>();
        value[0].Id.ShouldBe(Guid.Empty);
        _ = value[0].Title.ShouldBeOfType<string>();
        value[0].Title.ShouldBe("Lorem");
        _ = value[0].Description.ShouldBeOfType<string>();
        value[0].Description.ShouldBe("Ipsum");

    }
}
