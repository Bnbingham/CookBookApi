namespace CookBookApi.Application.Tests.Unit.CookBooks.Queries.GetCookBooks;

using System.Threading;
using System.Threading.Tasks;
using Application.CookBooks;
using Application.CookBooks.Entities;
using Application.CookBooks.Queries.GetCookBooks;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetCookBooksHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetCookBooksQuery();

        var context = Substitute.For<ICookBookRepository>();
        var handler = new GetCookBooksHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetCookBooks(token).Returns([new CookBook(Guid.Empty, "Title", "Description")]);

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetCookBooks(token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<List<CookBook>>();

        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);

        result[0].Id.ShouldBe(Guid.Empty);
        result[0].Title.ShouldBe("Title");
        result[0].Description.ShouldBe("Description");
    }
}
