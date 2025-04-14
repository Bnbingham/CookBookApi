namespace CookBookApi.Application.Tests.Unit.Authors.Queries.GetAuthorById;

using System.Threading;
using System.Threading.Tasks;
using Application.Authors;
using Application.Authors.Entities;
using Application.Authors.Queries.GetAuthorById;
using NSubstitute;
using Shouldly;
using Xunit;

public class GetAuthorByIdTests
{
    [Fact]
    public async Task Handle_ShouldPassThrough_Query()
    {
        // Arrange
        var query = new GetAuthorByIdQuery { Id = Guid.Empty };

        var context = Substitute.For<IAuthorsRepository>();
        var handler = new GetAuthorByIdHandler(context);
        var token = new CancellationTokenSource().Token;

        _ = context.GetAuthorById(Guid.Empty, token).Returns(new Author(Guid.Empty, "FirstName", "LastName", []));

        // Act
        var result = await handler.Handle(query, token);

        // Assert
        _ = await context.Received(1).GetAuthorById(Guid.Empty, token);

        _ = result.ShouldNotBeNull();
        _ = result.ShouldBeOfType<Author>();

        result.Id.ShouldBe(Guid.Empty);
        result.FirstName.ShouldBe("FirstName");
        result.LastName.ShouldBe("LastName");
    }
}
