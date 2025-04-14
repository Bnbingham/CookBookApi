namespace CookBookApi.Infrastructure.Tests.Integration.Databases.CookBooks;

using System;
using AutoMapper;
using Extensions;
using Infrastructure.Databases.CookBooks;
using Infrastructure.Databases.CookBooks.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using Xunit;

[CollectionDefinition("CookBooks")]
public class CookBooksCollectionFixture : ICollectionFixture<CookBooksDataFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class CookBooksDataFixture : IDisposable
{
    internal CookBooksDbContext Context { get; set; }
    internal FakeTimeProvider TimeProvider { get; set; }
    internal IMapper Mapper { get; set; }
    internal EntityFrameworkCookBookRepository Repository { get; set; }

    public CookBooksDataFixture()
    {
        var options = new DbContextOptionsBuilder<CookBooksDbContext>()
            .UseInMemoryDatabase($"TestCookBooks-{Guid.NewGuid()}")
            .Options;

        this.Context = new CookBooksDbContext(options);

        this.TimeProvider = new FakeTimeProvider();
        this.TimeProvider.SetUtcNow(new DateTime(2009, 12, 31, 23, 51, 01));

        this.Mapper = new MapperConfiguration(cfg =>
            cfg
                .AddProfiles(
                [
                    new AuthorMappingProfile(),
                    new CookBookMappingProfile(),
                    new IngredientMappingProfile(),
                    new RecipeMappingProfile()
                ]))
                .CreateMapper();

        this.Repository = new EntityFrameworkCookBookRepository(this.Context, this.TimeProvider, this.Mapper);

        if (this.Context != null)
        {
            _ = this.Context.Database.EnsureDeleted();
            _ = this.Context.Database.EnsureCreated();
            _ = this.Context.AddTestData();
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
                this.Context = null;
            }
        }
    }
}
