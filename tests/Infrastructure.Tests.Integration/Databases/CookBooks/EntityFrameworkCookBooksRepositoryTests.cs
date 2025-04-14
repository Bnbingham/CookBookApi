namespace CookBookApi.Infrastructure.Tests.Integration.Databases.CookBooks;

using Shouldly;
using Xunit;

[Collection("CookBooks")]
public class EntityFrameworkCookBooksRepositoryTests(CookBooksDataFixture fixture)
{

    #region Authors

    [Fact]
    public async Task GetAuthors_ShouldReturn_Authors()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.GetAuthors(token);

        // Assert
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(3);
    }


    [Fact]
    public async Task GetAuthorById_ShouldReturn_Author()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;
        var author = fixture.Context.Authors.FirstOrDefault(a => a.FirstName == "One");

        // Act
        var result = await repository.GetAuthorById(author.Id, token);

        // Assert
        _ = result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.FirstName.ShouldBe("One");
        result.LastName.ShouldBe("One");
    }

    [Fact]
    public async Task GetAuthorById_ShouldReturn_Null()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.GetAuthorById(Guid.Empty, token);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public async Task AuthorExists_ShouldReturn_True()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;
        var author = fixture.Context.Authors.FirstOrDefault(a => a.FirstName == "One");

        // Act
        var result = await repository.AuthorExists(author.Id, token);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorExists_ShouldReturn_False()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.AuthorExists(Guid.Empty, token);

        // Assert
        result.ShouldBeFalse();
    }

    #endregion Authors

    #region CookBooks

    [Fact]
    public async Task GetCookBooks_ShouldReturn_CookBooks()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.GetCookBooks(token);

        // Assert
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(3);
    }

    [Fact]
    public async Task GetCookBookById_ShouldReturn_CookBook()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;
        var cookBook = fixture.Context.CookBooks.FirstOrDefault(c => c.Title == "One");

        // Act
        var result = await repository.GetCookBookById(cookBook.Id, token);

        // Assert
        _ = result.ShouldNotBeNull();
        result.Id.ShouldNotBe(Guid.Empty);
        result.Title.ShouldBe("One");
    }

    [Fact]
    public async Task GetCookBookById_ShouldReturn_Null()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.GetCookBookById(Guid.Empty, token);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public async Task CookBookExists_ShouldReturn_True()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;
        var cookBook = fixture.Context.CookBooks.FirstOrDefault(c => c.Title == "One");

        // Act
        var result = await repository.CookBookExists(cookBook.Id, token);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task CookBookExists_ShouldReturn_False()
    {
        // Arrange
        var repository = fixture.Repository;
        var token = new CancellationTokenSource().Token;

        // Act
        var result = await repository.CookBookExists(Guid.Empty, token);

        // Assert
        result.ShouldBeFalse();
    }

    #endregion CookBooks
}
