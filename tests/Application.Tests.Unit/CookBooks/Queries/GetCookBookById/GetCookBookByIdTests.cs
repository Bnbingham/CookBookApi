namespace CookBookApi.Application.Tests.Unit.CookBooks.Queries.GetCookBookById;

using System.Threading;
using System.Threading.Tasks;
using Application.CookBooks;
using Application.CookBooks.Entities;
using Application.CookBooks.Queries.GetCookBookById;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetCookBookByIdTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetCookBookByIdQuery { Id = Guid.Empty };

        var context = Substitute.For<ICookBookRepository>();
        var handler = new GetCookBookByIdHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetCookBookById(Guid.Empty, token).Returns(new CookBook(Guid.Empty, "Title", "Description"));

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetCookBookById(Guid.Empty, token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<CookBook>();

        result.Id.ShouldBe(Guid.Empty);
        result.Title.ShouldBe("Title");
        result.Description.ShouldBe("Description");
    }
}
