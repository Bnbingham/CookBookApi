namespace CookBookApi.Infrastructure.Tests.Integration.Databases.CookBooks;

using AutoMapper;
using Xunit;

[Collection("CookBooks")]
public class MappingConfigurationTests(CookBooksDataFixture fixture)
{
    private readonly IMapper mapper = fixture.Mapper;

    [Fact]
    public void ShouldHaveValidMappingConfiguration()
    {
        this.mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
