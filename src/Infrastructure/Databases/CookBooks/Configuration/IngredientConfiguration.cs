namespace CookBookApi.Infrastructure.Databases.CookBooks.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class IngredientConfiguration : EntityConfiguration<Ingredient>
{
    public override void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        base.Configure(builder);
    }
}
