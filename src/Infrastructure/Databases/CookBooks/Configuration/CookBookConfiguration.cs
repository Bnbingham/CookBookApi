namespace CookBookApi.Infrastructure.Databases.CookBooks.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class CookBookConfiguration : EntityConfiguration<CookBook>
{
    public override void Configure(EntityTypeBuilder<CookBook> builder)
    {
        base.Configure(builder);

        _ = builder.HasMany(m => m.Recipes).WithMany(r => r.CookBooks);
    }
}
