namespace CookBookApi.Infrastructure.Databases.CookBooks.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class AuthorConfiguration : EntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);

        _ = builder.HasMany(m => m.Recipes).WithOne(r => r.Author).HasForeignKey(r => r.AuthorId);
    }
}
